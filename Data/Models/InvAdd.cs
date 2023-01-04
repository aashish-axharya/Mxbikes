using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxbikes.Data.Models
{
	public class InvAdd
	{
		public Guid ItemAddId { get; set; } = Guid.NewGuid();
		public Guid ItemId { get; set; }
		public int QuantityToAdd { get; set; }
		public Guid AddedBy { get; set; }
		public DateTime DateAdded { get; set; }

	}
}
