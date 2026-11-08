using CenterPro.Data.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Web.Controllers;

[Authorize]
public sealed class HomeController(CenterProDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var canManage = User.IsInRole("TrainingStaff") || User.IsInRole("Administrator");
        ViewBag.CanManage = canManage;
        if (canManage)
        {
            ViewBag.GiangVienCount = await db.GiangVien.CountAsync();
            ViewBag.PhongHocCount = await db.PhongHoc.CountAsync();
            ViewBag.ClassCount = await db.LopHoc.CountAsync();
        }
        return View();
    }

    [AllowAnonymous]
    public IActionResult AccessDenied() => View();

    [AllowAnonymous]
    public IActionResult Error() => View();
}
