using Hospital.DB.Model;

namespace Hospital.WEB.Models
{
    public class AppointmentModel
    {
        public int id { get; set; }
        public DateTime? date { get; set; } = DateTime.Now;
        public int? polyclinicId { get; set; }
        public string? polyclinicName { get; set; } //ekledik
        public int? doctorId { get; set; } //ekledik
        public string? doctorNameSurname { get; set; }
        public int? patientId { get; set; }
        public int? patientType { get; set; }

    }
}
