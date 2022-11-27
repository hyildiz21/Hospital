using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DB.Model
{
	public partial class Insurance
	{
		public int id { get; set; }
		public bool insurancesStatus { get; set; }
		public string? insurancesType { get; set; }
		public string? tc { get; set; }


	}
}
