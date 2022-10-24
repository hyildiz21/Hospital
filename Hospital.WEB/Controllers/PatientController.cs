using Hospital.DB;
using Hospital.DB.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WEB.Controllers
{
    public class PatientController : Controller
    {
        /// <summary>
        /// contexti bir kez oluşturup tekrar tekrar kullanabililelim.
        /// </summary>
        HospitalContext context;
        public PatientController()
        {
               context = new HospitalContext();
        }


        public IActionResult Index()
        {
            //doğum yerlerini seçenek olarak patient cont ta tanımlayıp indexe gönderebiliriz
            //gönderirsek index e de söylememiz gerek biz bunu bekliyoruz diye
            List<string>Iller=new List<string>();
            Iller.Add("İstanbul");
            Iller.Add("Ankara");
            Iller.Add("İzmir");
            Iller.Add("Samsun");
            Iller.Add("Çanakkale");
            ViewData["Iller"]=Iller;

            //Dbdeki poliklinik tablosundaki bölümleri çektik
            List<Polyclinic> policlinics = context.Polyclinics.ToList();
            ViewData["policlinics"] = policlinics;

            //Dbdeki doktor tablosundaki doktorları çektik
            List<Doctor> doctors = context.Doctors.ToList();
            ViewData["doctors"] = doctors;

            //daha sonra bu iki ifadeyi indeximize tanıtmamız lazım yukarıdaki illerdeki gibi


            return View();
        }

        public IActionResult Save(Patient patient)
        {
            HospitalContext context = new HospitalContext();
            context.Patients.Add(patient);
            try
            {
                context.SaveChanges();
                List<string> Iller = new List<string>();
                Iller.Add("İstanbul");
                Iller.Add("Ankara");
                Iller.Add("İzmir");
                Iller.Add("Samsun");
                Iller.Add("Çanakkale");
                ViewData["Iller"] = Iller;

                return View("Index");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
            
        }
    }
}
