using CenterPro.Data.Domain;
using CenterPro.Data.Infrastructure;
using CenterPro.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Web.Controllers;

[Authorize(Roles = "TrainingStaff,Administrator")]
public sealed class GiangVienController(CenterProDbContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.GiangVien.OrderBy(x => x.HoTen).ToListAsync());

    [HttpGet]
    public IActionResult Create() => View(new NhapGiangVienModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NhapGiangVienModel model)
    {
        if (!ModelState.IsValid) return View(model);
        db.GiangVien.Add(new GiangVien { HoTen = model.HoTen.Trim() });
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm giảng viên.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var entity = await db.GiangVien.FindAsync(id);
        ViewBag.Id = id;
        return entity is null ? NotFound() : View(new NhapGiangVienModel { HoTen = entity.HoTen });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NhapGiangVienModel model)
    {
        if (!ModelState.IsValid) { ViewBag.Id = id; return View(model); }
        var entity = await db.GiangVien.FindAsync(id);
        if (entity is null) return NotFound();
        entity.HoTen = model.HoTen.Trim();
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật giảng viên.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.GiangVien.FindAsync(id);
        return entity is null ? NotFound() : View(entity);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await db.GiangVien.FindAsync(id);
        if (entity is null) return NotFound();
        db.GiangVien.Remove(entity);
        try { await db.SaveChangesAsync(); TempData["Success"] = "Đã xóa giảng viên."; }
        catch (DbUpdateException) { TempData["Error"] = "Không thể xóa giảng viên đang được lớp học tham chiếu."; }
        return RedirectToAction(nameof(Index));
    }
}
