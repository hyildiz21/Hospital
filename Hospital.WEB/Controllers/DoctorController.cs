using Hospital.DB;
using Hospital.DB.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WEB.Controllers
{
	public class DoctorController : Controller
	{
		HospitalContext context;
		
		public DoctorController()
		{
			context = new HospitalContext();
		}

		public IActionResult Index(int id)
		{
			//appointmentlardan idsni çekip parametre olarak gelen hasta id si ile eşleştirdim
			Appointment appointment = context.Appointments.Where(x => x.id == id).FirstOrDefault();
			Patient patient = context.Patients.Where(x => x.id == appointment.patientId).FirstOrDefault();
			//arka tarafa göndericez bu patienti

			List<Analyz> analyzes = context.Analyzes.ToList();
			List<Medicine> medicines = context.Medicines.ToList();

			ViewData["Analyzes"] = analyzes;
			ViewData["Medicines"] = medicines;


			return View(patient);
		}


	}
}
