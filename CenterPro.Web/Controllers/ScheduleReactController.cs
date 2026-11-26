using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CenterPro.Web.Controllers;

[Authorize(Roles = "TrainingStaff,Administrator")]
public sealed class ScheduleReactController : Controller
{
    public IActionResult Index() => View("~/Views/Home/Schedule.cshtml");
}
