using CenterPro.Data.Domain;
using CenterPro.Data.Infrastructure;
using CenterPro.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Web.Controllers;

[Authorize(Roles = "TrainingStaff,Administrator")]
public sealed class LopHocController(CenterProDbContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.LopHoc.Include(x => x.GiangVien).OrderBy(x => x.TenLopHoc).ToListAsync());

    [HttpGet]
    public async Task<IActionResult> Create() { await LoadGiangVienAsync(); return View(new NhapLopHocModel()); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NhapLopHocModel model)
    {
        if (!ModelState.IsValid || !await db.GiangVien.AnyAsync(x => x.MaGiangVien == model.MaGiangVien))
        {
            if (ModelState.IsValid) ModelState.AddModelError(nameof(model.MaGiangVien), "Giảng viên được chọn không tồn tại.");
            await LoadGiangVienAsync(model.MaGiangVien);
            return View(model);
        }
        db.LopHoc.Add(new LopHoc { TenLopHoc = model.TenLopHoc.Trim(), MaGiangVien = model.MaGiangVien });
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã tạo lớp học.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var entity = await db.LopHoc.FindAsync(id);
        if (entity is null) return NotFound();
        ViewBag.Id = id;
        await LoadGiangVienAsync(entity.MaGiangVien);
        return View(new NhapLopHocModel { TenLopHoc = entity.TenLopHoc, MaGiangVien = entity.MaGiangVien });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NhapLopHocModel model)
    {
        if (!ModelState.IsValid || !await db.GiangVien.AnyAsync(x => x.MaGiangVien == model.MaGiangVien))
        {
            if (ModelState.IsValid) ModelState.AddModelError(nameof(model.MaGiangVien), "Giảng viên được chọn không tồn tại.");
            ViewBag.Id = id;
            await LoadGiangVienAsync(model.MaGiangVien);
            return View(model);
        }
        var entity = await db.LopHoc.FindAsync(id);
        if (entity is null) return NotFound();
        entity.TenLopHoc = model.TenLopHoc.Trim();
        entity.MaGiangVien = model.MaGiangVien;
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật lớp học.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.LopHoc.Include(x => x.GiangVien).FirstOrDefaultAsync(x => x.MaLopHoc == id);
        return entity is null ? NotFound() : View(entity);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await db.LopHoc.FindAsync(id);
        if (entity is null) return NotFound();
        db.LopHoc.Remove(entity);
        try { await db.SaveChangesAsync(); TempData["Success"] = "Đã xóa lớp học."; }
        catch (DbUpdateException) { TempData["Error"] = "Không thể xóa lớp đang được đề xuất hoặc lịch học tham chiếu."; }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadGiangVienAsync(int? selectedId = null) =>
        ViewBag.GiangVien = new SelectList(await db.GiangVien.OrderBy(x => x.HoTen).ToListAsync(), "MaGiangVien", "HoTen", selectedId);
}
