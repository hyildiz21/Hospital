using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB.Model
{
    public partial class DoctorRoom
    {
        public int id { get; set; }
        public int? doctorId { get; set; }
        public int? roomId { get; set; }
    }

}
