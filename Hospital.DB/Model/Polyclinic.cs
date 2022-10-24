using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB.Model
{
    public partial class Polyclinic
    {
        public int id { get; set; }
        [StringLength(150)] public string name { get; set; }
    }
}
