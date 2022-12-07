using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB.Model
{
	public partial class Medicine
	{
        public int id { get; set; }
        public string? name { get; set; }
        public decimal? price { get; set; }
    }
}
