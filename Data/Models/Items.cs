using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxbikes.Data.Models
{
    public class Items
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string TakenBy {get; set; }
        public Guid ApprovedBy { get; set; }
        public DateTime DateTaken { get; set; }

    }
}
