using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB.Model
{
    public partial class Appointment
    {
        public int id { get; set; }
        public DateTime date { get; set; }

        public int policlinicId { get; set; }
        public int doctorId { get; set; }
        public int patientId { get; set; }
        public int patientType { get; set; }

    }
}
