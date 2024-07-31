using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TonsNeutros.Admin.Models;

namespace TonsNeutros.Admin.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorsViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
