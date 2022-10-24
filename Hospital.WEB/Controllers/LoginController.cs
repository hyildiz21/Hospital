using Hospital.DB;
using Hospital.DB.Model;
using Hospital.WEB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hospital.WEB.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SignIn(SignInModel signIn)
        {
            //sign in yapısını contextten çekicez 
            HospitalContext context = new HospitalContext();
           
            //Önce bize yukarıdan gelen signInden gelen username ve passwordu DBdeki users tablosundan bulmamız gerek
            User user = context.Users.Where(x => x.username == signIn.username && x.password == signIn.password).FirstOrDefault();

            if (user != null)
            {
                var claim = new List<Claim>
                {new Claim(ClaimTypes.SerialNumber,user.id.ToString()),
                 new Claim(ClaimTypes.Name,user.nameSurname.ToString())};

                var userIdentity = new ClaimsIdentity(claim, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");

            }
            //Eğer ki user boş gelirse de login ekranına tekrar çıkalım
            else
            {
                return View("Index");
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
