using System;
using CICD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using CICD.Helpers;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace CICD.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    public APIHelper apiHelper;

    public AccountController(ILogger<AccountController> logger)
    {
        this.apiHelper = new APIHelper();
        _logger = logger;
    }

    public IActionResult Login()
    {
        if (true)
        {
            return RedirectToAction("Home", "WebApp");
        }
        //ClaimsPrincipal claimUser = HttpContext.User;
        //if (claimUser.Identity.IsAuthenticated)
        //{
        //    var accessToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
        //    var systemToken = string.IsNullOrEmpty(HttpContext.User.FindFirstValue("systemToken")) ? accessToken : HttpContext.User.FindFirstValue("systemToken");
        //    if (this.apiHelper.IsValid(systemToken))
        //    {
        //        return RedirectToAction("Home", "WebApp");
        //    }
        //}
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Login login)
    {
        //AuthenticationResponseData authenticationResponse = new AuthenticationResponseData();
        //string token = this.apiHelper.GetToken();
        //if (this.apiHelper.IsValid(token))
        //{
        //    authenticationResponse = this.apiHelper.Authentication(login, token);
        //    if (authenticationResponse.Data.IsAuthenticated)
        //    {
        //        List<Claim> claim = new List<Claim>()
        //        {
        //            new Claim(ClaimTypes.NameIdentifier, login.UserName),
        //            new Claim("user", login.UserName.ToString()),
        //            new Claim("systemToken", token.ToString()),
        //            new Claim("userRole", authenticationResponse.Data.RoleName)
        //        };
        //        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
        //            claim,
        //            CookieAuthenticationDefaults.AuthenticationScheme
        //        );
        //        AuthenticationProperties authenticationProperties = new AuthenticationProperties()
        //        {
        //            AllowRefresh = true,
        //            IsPersistent = login.KeepLoginIn
        //        };

        //        await HttpContext.SignInAsync(
        //            CookieAuthenticationDefaults.AuthenticationScheme,
        //            new ClaimsPrincipal(claimsIdentity),
        //            authenticationProperties
        //        );
        //        return RedirectToAction("Home", "WebApp");
        //    }
        //}
        //ViewData["ValidateMessage"] = authenticationResponse.Data.Message;
        return View();
    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(
    //        new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
    //    );
    //}
}
