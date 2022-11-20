using Hospital.DB;
using Hospital.DB.Model;
using Hospital.WEB.Models;
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

            polyclinics = context.Polyclinics.ToList();
            doctors = context.Doctors.ToList();

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
            int patientId;
            int age;

            //gelen hasta bilgisine göre karşılaştırma yapıp böyle bir hasta var mı yok mu kontrol ediyoruz
            //öncelikle eğer aynı tc li hastam kayıtlı ise bir daha yeni kayıt açmamak için kontrol
            Patient oldpatient = context.Patients.Where(x => x.tc == patient.tc).FirstOrDefault();

            
            if (oldpatient!=null) //hastanın var olma durumunda
            {
                patientId = oldpatient.id;
                age = (int)(DateTime.Now - Convert.ToDateTime(oldpatient.birthDate)).TotalDays / 365; //hasta yaşı
            }
            else
            {
                //hastaları ekledik tabloya
                var response = context.Patients.Add(patient);
                context.SaveChanges();

                //daha sonra randevu tablosundaki hasta id ile yukarda gelen hasta idyi eşitledik
                patientId = response.Entity.id;

                age =(int)(DateTime.Now - Convert.ToDateTime(patient.birthDate)).TotalDays / 365; //hasta yaşı

            }

            appointment.patientId = patientId;

             
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
            Patient patient = context.Patients.Where(x => x.tc == tc).FirstOrDefault();
     
            if (patient == null)
            {
                return Json(null); //!!!önemli json deönmeli
            }

            List<Appointment> appointments = context.Appointments.Where(x => x.date.Value.Date == DateTime.Now.Date && x.patientId == patient.id).ToList();

            List<AppointmentModel> appointmentModels=new List<AppointmentModel>();
            foreach (var item in appointments)
            {
                AppointmentModel appointmentModel = new AppointmentModel
                {
                    id = item.id,
                    date = item.date,
                    polyclinicId = item.polyclinicId,
                    doctorId = item.doctorId,
                    patientId=item.patientId,
                    patientType=item.patientType,
                    //doktorun ad ve soyadı şimdi
                    doctorNameSurname=doctors.Where(x=>x.id==item.doctorId).FirstOrDefault().name + " " + doctors.Where(x=>x.id==item.doctorId).FirstOrDefault().surname,
                    //poliklinik ismi yazdırdık
                    polyclinicName=polyclinics.Where(x=>x.id==item.polyclinicId).FirstOrDefault().name

                };
                appointmentModels.Add(appointmentModel); 
            }
            



            return Json(appointmentModels);

            //return Json(appointments);


        }

        public JsonResult GetPatient(string tc)
        {
            return Json(context.Patients.Where(x => x.tc == tc).FirstOrDefault());

        }


    }



}
