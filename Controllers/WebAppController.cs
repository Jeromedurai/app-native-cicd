using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CICD.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CICD.Helpers;

namespace CICD.Controllers;

//[Authorize]
public class WebAppController : Controller
{
    private readonly ILogger<WebAppController> _logger;
    public APIHelper apiHelper;
    public WebAppController(ILogger<WebAppController> logger)
    {
        //this.apiHelper = new APIHelper();
        //_logger = logger;
    }
    public IActionResult Home()
    {
        //ClaimsPrincipal claimUser = HttpContext.User;
        //if (claimUser.Identity.IsAuthenticated)
        //{
        //    ViewData["userRole"] = HttpContext.User.FindFirstValue("userRole");
        //    ViewData["systemToken"] = HttpContext.User.FindFirstValue("systemToken");
        //    ViewData["user"] = HttpContext.User.FindFirstValue("user");
        //}
        return View();
    }

    //public IActionResult Privacy()
    //{
    //    return View();
    //}

    //public async Task<IActionResult> LogOut()
    //{

    //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //    return RedirectToAction("Login", "Account");
    //}

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}
}
