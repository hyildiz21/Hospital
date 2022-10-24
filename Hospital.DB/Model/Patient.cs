using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB.Model
{
    public partial class Patient
    {
        public int id { get; set; }

        [StringLength(150)] public string name { get; set; }
        [StringLength(150)] public string surname { get; set; }
        
        //int64 verme nedenimiz 11 haneli tc girince int in sayısal değer aralığından fazla oluo
        public Int64 tc { get; set; }
        [StringLength(20)] public string phone { get; set; }
        [StringLength(150)] public string mail { get; set; }
        public DateTime birthDate { get; set; }
        [StringLength(80)] public string birthPlace { get; set; }
    }
}
