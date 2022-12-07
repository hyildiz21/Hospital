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
        private HospitalContext context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            context = new HospitalContext();
        }

        public IActionResult Index()
        {
            
           
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

        public JsonResult GetAppointment() 
        {
            //User id yi öğrenmemiz gerek içeri kim giriyo claimslerdeki id sayesinde
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.SerialNumber).FirstOrDefault().Value;
            
            //doctorId yi appoinmentlardan çektim yukardaki userId ile eşitleyip.
            int? doctorId = context.Users.Where(x => x.id == Convert.ToInt32(userId)).FirstOrDefault().doctorId;

            List<AppointmentModel> appointmentModels = new List<AppointmentModel>();

            //randevuları bir listeye çekelim -yukarıdaki doctorId ye sahip doktorun randvularını yani-
            List<Appointment> appointments = context.Appointments.Where(x => x.doctorId == doctorId).ToList();


            foreach (var item in appointments)
            {
                //hastayı 1 kere çektim bunu tc isim soyisim ve doğum tarihini karşıay göndermek için kullanıcam aşağıda           
                Patient patient = context.Patients.Where(x => x.id == item.patientId).FirstOrDefault();

                AppointmentModel appointmentModel = new AppointmentModel
                {
                    id=item.id,
                    date = item.date,
                    complaint=item.complaint,
                    doctorId = item.doctorId,
                    patientId = item.patientId,
                    patientType = item.patientType,
                    patientTc = patient.tc,
                    patientBirthDate = patient.birthDate,
                    patientNameSurname = patient.name + " " + patient.surname,
                };
                
                appointmentModels.Add(appointmentModel);
                
            }

            return Json(appointmentModels);
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