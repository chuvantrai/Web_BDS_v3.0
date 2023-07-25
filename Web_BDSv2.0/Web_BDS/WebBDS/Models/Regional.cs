using System;
using System.Collections.Generic;

namespace WebBDS.Models
{
    public partial class Regional
    {
        public Regional()
        {
            Products = new HashSet<Product>();
        }

        public int RegionalId { get; set; }
        public string RegionalName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
