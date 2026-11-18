
using CenterPro.Data.Domain;
using CenterPro.Data.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Web.Controllers;

[Authorize(Roles = "TrainingStaff,Administrator")]
public sealed class XuLyDeXuatLichController(CenterProDbContext db) : Controller
{
    // Danh sách đề xuất lịch dạy
    public async Task<IActionResult> Index(string? trangThai = null)
    {
        var query = db.DeXuatLichDay
            .AsNoTracking()
            .Include(x => x.LopHoc)
            .Include(x => x.GiangVien)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(trangThai))
        {
            query = query.Where(x => x.TrangThai == trangThai);
        }

        var danhSach = await query
            .OrderByDescending(x => x.ThoiGianGui)
            .ToListAsync();

        ViewBag.TrangThaiHienTai = trangThai;
        return View(danhSach);
    }

    // Xem chi tiết đề xuất
    [HttpGet]
    public async Task<IActionResult> XuLy(int id)
    {
        var deXuat = await db.DeXuatLichDay
            .Include(x => x.LopHoc)
            .Include(x => x.GiangVien)
            .FirstOrDefaultAsync(x => x.MaDeXuatLich == id);

        if (deXuat == null)
            return NotFound();

        if (deXuat.TrangThai != "Pending")
        {
            TempData["Error"] = "Đề xuất này đã được xử lý rồi.";
            return RedirectToAction(nameof(Index));
        }

        await LoadPhongTrongAsync(
            deXuat.ThoiGianBatDauDeXuat,
            deXuat.ThoiGianKetThucDeXuat,
            deXuat.MaGiangVien);

        return View(deXuat);
    }

    // Xác nhận đề xuất và tạo lịch học
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> XacNhan(int id, int maPhongHoc)
    {
        var deXuat = await db.DeXuatLichDay
            .Include(x => x.LopHoc)
            .Include(x => x.GiangVien)
            .FirstOrDefaultAsync(x => x.MaDeXuatLich == id);

        if (deXuat == null)
            return NotFound();

        if (deXuat.TrangThai != "Pending")
        {
            TempData["Error"] = "Đề xuất đã được xử lý.";
            return RedirectToAction(nameof(Index));
        }

        if (deXuat.ThoiGianKetThucDeXuat <= deXuat.ThoiGianBatDauDeXuat)
        {
            TempData["Error"] = "Thời gian đề xuất không hợp lệ.";
            return RedirectToAction(nameof(Index));
        }

        // Kiểm tra phòng học được chọn có tồn tại không
        bool phongTonTai = await db.PhongHoc
            .AnyAsync(x => x.MaPhongHoc == maPhongHoc);

        if (!phongTonTai)
        {
            TempData["Error"] = "Phòng học không tồn tại. Vui lòng chọn lại.";
            await LoadPhongTrongAsync(
                deXuat.ThoiGianBatDauDeXuat,
                deXuat.ThoiGianKetThucDeXuat,
                deXuat.MaGiangVien);

            return View("XuLy", deXuat);
        }

        // Kiểm tra giảng viên đã có lịch trong khoảng thời gian này chưa
        bool trungGiangVien = await db.LichHoc.AnyAsync(x =>
            x.LopHoc.MaGiangVien == deXuat.MaGiangVien &&
            x.ThoiGianBatDau < deXuat.ThoiGianKetThucDeXuat &&
            x.ThoiGianKetThuc > deXuat.ThoiGianBatDauDeXuat);

        if (trungGiangVien)
        {
            TempData["Error"] = "Giảng viên đã có lịch dạy trong khung giờ này.";
            await LoadPhongTrongAsync(
                deXuat.ThoiGianBatDauDeXuat,
                deXuat.ThoiGianKetThucDeXuat,
                deXuat.MaGiangVien);

            return View("XuLy", deXuat);
        }

        // Kiểm tra phòng học có bị trùng lịch không
        bool trungPhong = await db.LichHoc.AnyAsync(x =>
            x.MaPhongHoc == maPhongHoc &&
            x.ThoiGianBatDau < deXuat.ThoiGianKetThucDeXuat &&
            x.ThoiGianKetThuc > deXuat.ThoiGianBatDauDeXuat);

        if (trungPhong)
        {
            TempData["Error"] = "Phòng học đã có lớp khác sử dụng trong khung giờ này.";
            await LoadPhongTrongAsync(
                deXuat.ThoiGianBatDauDeXuat,
                deXuat.ThoiGianKetThucDeXuat,
                deXuat.MaGiangVien);

            return View("XuLy", deXuat);
        }

        // Tạo lịch học sau khi tất cả điều kiện hợp lệ
        var lichHoc = new LichHoc
        {
            MaLopHoc = deXuat.MaLopHoc,
            MaPhongHoc = maPhongHoc,
            MaDeXuatLich = deXuat.MaDeXuatLich,
            ThoiGianBatDau = deXuat.ThoiGianBatDauDeXuat,
            ThoiGianKetThuc = deXuat.ThoiGianKetThucDeXuat
        };

        deXuat.TrangThai = "Approved";
        db.LichHoc.Add(lichHoc);

        await db.SaveChangesAsync();

        TempData["Success"] = "Đã xác nhận đề xuất và tạo lịch học thành công.";
        return RedirectToAction(nameof(Index));
    }

    // Từ chối đề xuất
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TuChoi(int id)
    {
        var deXuat = await db.DeXuatLichDay
            .FirstOrDefaultAsync(x => x.MaDeXuatLich == id);

        if (deXuat == null)
            return NotFound();

        if (deXuat.TrangThai != "Pending")
        {
            TempData["Error"] = "Đề xuất đã được xử lý.";
            return RedirectToAction(nameof(Index));
        }

        deXuat.TrangThai = "Rejected";
        await db.SaveChangesAsync();

        TempData["Success"] = "Đã từ chối đề xuất.";
        return RedirectToAction(nameof(Index));
    }

    // Lấy danh sách phòng không bị trùng lịch.
    // Nếu giảng viên đã có lịch trong khung giờ đó thì không hiển thị phòng.
    private async Task LoadPhongTrongAsync(
        DateTime batDau,
        DateTime ketThuc,
        int maGiangVien)
    {
        bool giangVienBiTrung = await db.LichHoc.AnyAsync(x =>
            x.LopHoc.MaGiangVien == maGiangVien &&
            x.ThoiGianBatDau < ketThuc &&
            x.ThoiGianKetThuc > batDau);

        ViewBag.GiangVienBiTrung = giangVienBiTrung;

        if (giangVienBiTrung)
        {
            ViewBag.PhongHoc = new SelectList(
                Enumerable.Empty<SelectListItem>());

            return;
        }

        var phongTrong = await db.PhongHoc
            .Where(p => !db.LichHoc.Any(x =>
                x.MaPhongHoc == p.MaPhongHoc &&
                x.ThoiGianBatDau < ketThuc &&
                x.ThoiGianKetThuc > batDau))
            .OrderBy(p => p.TenPhongHoc)
            .ToListAsync();

        ViewBag.PhongHoc = new SelectList(
            phongTrong,
            "MaPhongHoc",
            "TenPhongHoc");
    }
}
