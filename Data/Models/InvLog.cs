using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxbikes.Data.Models
{
	public class InvLog
	{
		public Guid ID { get; set; } = Guid.NewGuid();
		public Guid ItemId { get; set; }
		public int Quantity { get; set; }
		public Guid Staffid { get; set; }
		public DateTime DateTaken { get; set; }
		public bool ApprovedStatus { get; set; } = false;
		public Guid ApprovedBy { get; set; }
	}
}
