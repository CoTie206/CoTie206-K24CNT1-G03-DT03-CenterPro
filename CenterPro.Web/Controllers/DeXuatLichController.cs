using CenterPro.Data.Domain;
using CenterPro.Data.Infrastructure;
using CenterPro.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Web.Controllers;

[Authorize(Roles = "Lecturer")]
public sealed class DeXuatLichController : Controller
{
    private readonly CenterProDbContext _db;
    private readonly UserManager<NguoiDungUngDung> _userManager;

    public DeXuatLichController(CenterProDbContext db, UserManager<NguoiDungUngDung> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var maGiangVien = await GetMaGiangVienAsync();
        if (maGiangVien == null)
            return RedirectToAction("AccessDenied", "Home");

        var list = await _db.DeXuatLichDay
            .Include(x => x.LopHoc)
            .Where(x => x.MaGiangVien == maGiangVien)
            .OrderByDescending(x => x.ThoiGianGui)
            .ToListAsync();

        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var maGiangVien = await GetMaGiangVienAsync();
        if (maGiangVien == null)
            return RedirectToAction("AccessDenied", "Home");

        await LoadLopHocAsync(maGiangVien.Value);
        return View(new NhapDeXuatLichModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NhapDeXuatLichModel model)
    {
        var maGiangVien = await GetMaGiangVienAsync();
        if (maGiangVien == null)
            return RedirectToAction("AccessDenied", "Home");

        var lopOk = await _db.LopHoc.AnyAsync(x =>
            x.MaLopHoc == model.MaLopHoc && x.MaGiangVien == maGiangVien);

        if (!lopOk)
            ModelState.AddModelError(nameof(model.MaLopHoc), "Lớp học không thuộc giảng viên của bạn.");

        if (model.ThoiGianBatDauDeXuat.HasValue &&
            model.ThoiGianKetThucDeXuat.HasValue &&
            model.ThoiGianKetThucDeXuat <= model.ThoiGianBatDauDeXuat)
        {
            ModelState.AddModelError(nameof(model.ThoiGianKetThucDeXuat),
                "Thời gian kết thúc phải sau thời gian bắt đầu.");
        }

        if (!ModelState.IsValid)
        {
            await LoadLopHocAsync(maGiangVien.Value, model.MaLopHoc);
            return View(model);
        }

        _db.DeXuatLichDay.Add(new DeXuatLichDay
        {
            MaLopHoc = model.MaLopHoc,
            MaGiangVien = maGiangVien.Value,
            ThoiGianBatDauDeXuat = model.ThoiGianBatDauDeXuat!.Value,
            ThoiGianKetThucDeXuat = model.ThoiGianKetThucDeXuat!.Value,
            TrangThai = "Pending",
            ThoiGianGui = DateTime.Now
        });

        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã gửi đề xuất lịch dạy thành công.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<int?> GetMaGiangVienAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        return user?.MaGiangVien;
    }

    private async Task LoadLopHocAsync(int maGiangVien, int? selected = null)
    {
        var data = await _db.LopHoc
            .Where(x => x.MaGiangVien == maGiangVien)
            .OrderBy(x => x.TenLopHoc)
            .ToListAsync();

        ViewBag.LopHoc = new SelectList(data, "MaLopHoc", "TenLopHoc", selected);
    }
}