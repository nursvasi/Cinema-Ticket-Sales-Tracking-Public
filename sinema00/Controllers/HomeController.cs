using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using sinema00.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace sinema00.Controllers
{
    [Authorize]//3.adım: eğer login durumundaysan sadece Home a girebilirsin.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly sinema00Context _sinema00Context ;
        public HomeController(ILogger<HomeController> logger, sinema00Context sinema00Context)
        {
            _logger = logger;
            _sinema00Context = sinema00Context;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}