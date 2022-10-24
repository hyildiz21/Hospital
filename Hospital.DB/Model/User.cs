using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB.Model
{
    public partial class User
    {
        //[Key]
        //[Column(Order = 1)]
        public int id { get; set; }

        [StringLength(100)] public string username { get; set; }

        [StringLength(100)] public string password { get; set; }
        public int userTypeId { get; set; }
        public int? doctorId { get; set; }

        [StringLength(150)] public string nameSurname { get; set; }







    }
}
