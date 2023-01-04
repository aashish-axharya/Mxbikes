using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxbikes.Data.Models
{
    public class Items
    {
        public Guid ItemId { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public DateTime LastTaken { get; set; }

    }
}
