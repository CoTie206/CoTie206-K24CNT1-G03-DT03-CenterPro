using CenterPro.Data.Domain;
using CenterPro.Data.Infrastructure;
using CenterPro.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Web.Controllers;

[Authorize(Roles = "TrainingStaff,Administrator")]
public sealed class PhongHocController(CenterProDbContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.PhongHoc.OrderBy(x => x.TenPhongHoc).ToListAsync());

    [HttpGet]
    public IActionResult Create() => View(new NhapPhongHocModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NhapPhongHocModel model)
    {
        if (!ModelState.IsValid) return View(model);
        db.PhongHoc.Add(new PhongHoc { TenPhongHoc = model.TenPhongHoc.Trim() });
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm phòng học.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var entity = await db.PhongHoc.FindAsync(id);
        ViewBag.Id = id;
        return entity is null ? NotFound() : View(new NhapPhongHocModel { TenPhongHoc = entity.TenPhongHoc });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NhapPhongHocModel model)
    {
        if (!ModelState.IsValid) { ViewBag.Id = id; return View(model); }
        var entity = await db.PhongHoc.FindAsync(id);
        if (entity is null) return NotFound();
        entity.TenPhongHoc = model.TenPhongHoc.Trim();
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật phòng học.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.PhongHoc.FindAsync(id);
        return entity is null ? NotFound() : View(entity);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await db.PhongHoc.FindAsync(id);
        if (entity is null) return NotFound();
        db.PhongHoc.Remove(entity);
        try { await db.SaveChangesAsync(); TempData["Success"] = "Đã xóa phòng học."; }
        catch (DbUpdateException) { TempData["Error"] = "Không thể xóa phòng đang được lịch học tham chiếu."; }
        return RedirectToAction(nameof(Index));
    }
}
