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

        //Dbdeki poliklinik tablosundaki bölümleri çektik
        List<Polyclinic> polyclinics;

        //Dbdeki doktor tablosundaki doktorları çektik
        List<Doctor> doctors;

        List<string> Iller;


        public PatientController()
        {
            context = new HospitalContext();
            Iller = new List<string>();


            //doğum yerlerini seçenek olarak patient cont ta tanımlayıp indexe gönderebiliriz
            //gönderirsek index e de söylememiz gerek biz bunu bekliyoruz diye

            Iller.Add("İstanbul");
            Iller.Add("Ankara");
            Iller.Add("İzmir");
            Iller.Add("Samsun");
            Iller.Add("Çanakkale");

            polyclinics= context.Polyclinics.ToList();
            doctors= context.Doctors.ToList();

        }

        public IActionResult Index()
        {

            ViewData["Iller"] = Iller;

            ViewData["policlinics"] = polyclinics;

            ViewData["doctors"] = doctors;

            //daha sonra bu iki ifadeyi indeximize tanıtmamız lazım yukarıdaki illerdeki gibi

            return View();
        }

        //hasta bilgisi ve randevu bilgisini almak için 2 parametre verdik
        public IActionResult Save(Patient patient, Appointment appointment)
        {
            //hastaları ekledik tabloya
            var response= context.Patients.Add(patient);
            context.SaveChanges();

            //daha sonra randevu tablosundaki hasta id ile yukarda gelen hasta idyi eşitledik
            appointment.patientId = response.Entity.id;
            
            int age = (int)(DateTime.Now - Convert.ToDateTime(patient.birthDate)).TotalDays / 365; //hasta yaşı
            //tek satırda if kontrolü
            appointment.patientType = age > 65 ? 1 : 2;

            //randevuları ekleyeceğiz ama öncesinde contexti savechangeslememiz gerek çünkü hasta id oluşsun
            context.Appointments.Add(appointment);

            try
            {
                context.SaveChanges();

                ViewData["Iller"] = Iller;

                //Dbdeki poliklinik tablosundaki bölümleri çektik
                ViewData["policlinics"] = polyclinics;

                //Dbdeki doktor tablosundaki doktorları çektik
                ViewData["doctors"] = doctors;

                return View("Index");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        public JsonResult GetDoctor(string polyclinicId)
        {

            return Json(doctors.Where(x => x.polyclinicId == Convert.ToInt32(polyclinicId)).ToList());

         }

        public JsonResult GetAppointment(string tc)
        {
            //çekilen tc ye ait olan hastayı çektik bütün hastaları değil hasta bilgilerini çektik
            Patient patient= context.Patients.Where(x => x.tc ==tc).FirstOrDefault();
            if (patient == null)
            {
                return null;
            }
            List<Appointment> appointments;

            try
            {
                appointments= context.Appointments.Where(x => x.date.Value.Date == DateTime.Now.Date && x.patientId == patient.id).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(appointments);
        }

    }



}
