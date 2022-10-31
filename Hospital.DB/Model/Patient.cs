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

        [StringLength(150)] public string? name { get; set; }
        [StringLength(150)] public string? surname { get; set; }
       
        [StringLength(11)] public string? tc { get; set; }
        [StringLength(20)] public string? phone { get; set; }
        [StringLength(150)] public string? mail { get; set; }
        public DateTime? birthDate { get; set; }
        [StringLength(80)] public string? birthPlace { get; set; }
        [StringLength(150)] public string? adress { get; set; }
    }
}
