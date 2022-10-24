using Hospital.DB;
using Hospital.DB.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WEB.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Save(Patient patient)
        {
            HospitalContext context = new HospitalContext();
            context.Patients.Add(patient);
            try
            {
                context.SaveChanges();
                return View("Index");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
            
        }
    }
}
