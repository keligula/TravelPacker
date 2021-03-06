using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Okta.AspNetCore;

namespace TravelPacker.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Challenge(OpenIdConnectDefaults.AuthenticationScheme);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            return new SignOutResult(new[]
            {
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme
            });
        }

        // [HttpPost]
        // public IActionResult Logout()
        // {
        //     return new SignOutResult(
        //         new[]
        //         {
        //                 OpenIdConnectDefaults.AuthenticationScheme,
        //                 CookieAuthenticationDefaults.AuthenticationScheme,
        //     },
        //         new AuthenticationProperties { RedirectUri = "/Home/Index" });
        // }

    }
}