using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB.Model
{
    public partial class DoctorTitle
    {
        public int id { get; set; }
        public string title { get; set; }
        public decimal titleAmount { get; set; }


    }
}
