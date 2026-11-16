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
	// 1. Danh sách đề xuất
	public async Task<IActionResult> Index(string? trangThai = null)
	{
		var query = db.DeXuatLichDay
			.Include(x => x.LopHoc)
			.Include(x => x.GiangVien)
			.AsQueryable();

		if (!string.IsNullOrEmpty(trangThai))
			query = query.Where(x => x.TrangThai == trangThai);

		var list = await query
			.OrderByDescending(x => x.ThoiGianGui)
			.ToListAsync();

		ViewBag.TrangThaiHienTai = trangThai;
		return View(list);
	}

	// 2. Xem chi tiết + form xác nhận
	[HttpGet]
	public async Task<IActionResult> XuLy(int id)
	{
		var deXuat = await db.DeXuatLichDay
			.Include(x => x.LopHoc)
			.Include(x => x.GiangVien)
			.FirstOrDefaultAsync(x => x.MaDeXuatLich == id);

		if (deXuat is null) return NotFound();
		if (deXuat.TrangThai != "Pending")
		{
			TempData["Error"] = "Đề xuất này đã được xử lý rồi.";
			return RedirectToAction(nameof(Index));
		}

		// Load danh sách phòng còn trống trong khung giờ đó
		await LoadPhongTrongAsync(deXuat.ThoiGianBatDauDeXuat, deXuat.ThoiGianKetThucDeXuat);
		return View(deXuat);
	}

	// 3. Xác nhận đề xuất → tạo lịch
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> XacNhan(int id, int maPhongHoc)
	{
		var deXuat = await db.DeXuatLichDay
			.Include(x => x.LopHoc)
			.FirstOrDefaultAsync(x => x.MaDeXuatLich == id);

		if (deXuat is null) return NotFound();
		if (deXuat.TrangThai != "Pending")
		{
			TempData["Error"] = "Đề xuất đã được xử lý.";
			return RedirectToAction(nameof(Index));
		}

		// ===== Kiểm tra chống trùng =====
		// 1. Trùng phòng
		bool trungPhong = await db.LichHoc.AnyAsync(x =>
			x.MaPhongHoc == maPhongHoc &&
			x.ThoiGianBatDau < deXuat.ThoiGianKetThucDeXuat &&
			x.ThoiGianKetThuc > deXuat.ThoiGianBatDauDeXuat);

		if (trungPhong)
		{
			TempData["Error"] = "Phòng học bị trùng lịch trong khung giờ này.";
			await LoadPhongTrongAsync(deXuat.ThoiGianBatDauDeXuat, deXuat.ThoiGianKetThucDeXuat);
			return View("XuLy", deXuat);
		}

		// 2. Trùng giảng viên
		bool trungGiangVien = await db.LichHoc.AnyAsync(x =>
			x.LopHoc.MaGiangVien == deXuat.MaGiangVien &&
			x.ThoiGianBatDau < deXuat.ThoiGianKetThucDeXuat &&
			x.ThoiGianKetThuc > deXuat.ThoiGianBatDauDeXuat);

		if (trungGiangVien)
		{
			TempData["Error"] = "Giảng viên đã có lịch dạy trong khung giờ này.";
			await LoadPhongTrongAsync(deXuat.ThoiGianBatDauDeXuat, deXuat.ThoiGianKetThucDeXuat);
			return View("XuLy", deXuat);
		}

		// ===== Tạo lịch học =====
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

	// 4. Từ chối đề xuất
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> TuChoi(int id)
	{
		var deXuat = await db.DeXuatLichDay.FindAsync(id);
		if (deXuat is null) return NotFound();

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

	private async Task LoadPhongTrongAsync(DateTime batDau, DateTime ketThuc)
	{
		var phongBiTrung = await db.LichHoc
			.Where(x => x.ThoiGianBatDau < ketThuc && x.ThoiGianKetThuc > batDau)
			.Select(x => x.MaPhongHoc)
			.ToListAsync();

		var phongTrong = await db.PhongHoc
			.Where(p => !phongBiTrung.Contains(p.MaPhongHoc))
			.OrderBy(p => p.TenPhongHoc)
			.ToListAsync();

		ViewBag.PhongHoc = new SelectList(phongTrong, "MaPhongHoc", "TenPhongHoc");
	}
}