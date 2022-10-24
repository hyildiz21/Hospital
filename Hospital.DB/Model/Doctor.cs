using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB.Model
{
    public partial class Doctor
    {
        public int id { get; set; }
        [StringLength(80)] public string name { get; set; }
        [StringLength(80)] public string surname { get; set; }
        [StringLength(20)] public string phone { get; set; }
        [StringLength(150)] public string mail { get; set; }

        public int titleId{ get; set; }
        public int polyclinicId{ get; set; }
        public int age{ get; set; }


    }
}
