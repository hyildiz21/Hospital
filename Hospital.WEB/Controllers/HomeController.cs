using Hospital.DB;
using Hospital.DB.Model;
using Hospital.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Hospital.WEB.Controllers
{
    [Authorize] //sadece yetkililer girebilir.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HospitalContext context=new HospitalContext();
           
            //User id yi öğrenmemiz gerek içeri kim giriyo claimslerdeki id sayesinde
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.SerialNumber).FirstOrDefault().Value;
           
            //hangi user ın login olup olmadığına baktık idlerden
            User user= context.Users.Where(x=>x.id==Convert.ToInt32(userId)).FirstOrDefault();

            if (user != null)
            {
                switch (user.userTypeId)
                {
                    //giriş yapıldıktan sonra ekrana doktor mu kayıt elemanı mı olduğunu yazan caseler
                    case 1:
                        ViewData["usertype"] = context.UserTypes.Where(x=>x.id==1).FirstOrDefault().type;
                        break;
                    case 2:
                        ViewData["usertype"] = context.UserTypes.Where(x => x.id == 2).FirstOrDefault().type; ;
                        break;

                    default:
                        ViewData["usertype"] = "--";
                        break;
                }
                return View(user);
            }
            else
            {
                //hatalı dönüs yani user boş gelirse
                return BadRequest("Kullanıcı bulunamadı tekrar giriş yapın.");
            }

            
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