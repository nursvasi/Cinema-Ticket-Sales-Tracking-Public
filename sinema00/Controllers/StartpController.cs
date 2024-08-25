using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using sinema00.Models;
using System.Security.Claims;

namespace sinema00.Controllers
{
    public class StartpController : Controller
    {
      //  Login action metodunu yaz

        [HttpPost]
        public async Task<IActionResult> Login(Logincs logincs)
        {
            //bu kısımda linq kullanarak veritabanındaki kayıtlarla doğruluk karşılaştırması yapın
            if (logincs.Email == "nursultanvasi@hotmail.com" && logincs.PassWord == "1234")
            {
                List<Claim> claims = new List<Claim>()
                {
                new Claim(ClaimTypes.NameIdentifier, logincs.Email),
                new Claim("DiğerÖzellikler","Örnek Rol")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
                //Gerekli Taleplerle bir ClaimsIdentity oluşturun ve kullanıcıda oturum açmak için SignInAsync’i çağırın:
                AuthenticationProperties prop = new AuthenticationProperties()
                {
                    AllowRefresh = true, // Refreshing the authentication session should be allowed
                    IsPersistent = logincs.LoggedStatus//Kimlik doğrulama oturumunun birden çok istekte kalıcı olup olmadığını belirlemek içindir.
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), prop);
                return RedirectToAction("Index", "Home");
            }// // Connect the existing harici cookie ve bilgileri yazar
            ViewData["OnayMesaji"] = "Kullanıcı Bulunamadı";
            return View();
        }


        //3.adım Login metodunun içeriğini oluştur.

        public IActionResult Login()//_Layout eklentisinden sonra View yaratın

        {//kullanıcının zaten login durumunda olup olmadığını kontrol et.

            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
}
}
