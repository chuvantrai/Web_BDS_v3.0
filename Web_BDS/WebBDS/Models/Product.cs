using System;
using System.Collections.Generic;

namespace WebBDS.Models
{
    public partial class Product
    {
        public Product()
        {
            ImageProducts = new HashSet<ImageProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public int RegionalId { get; set; }
        public string LetterPrice { get; set; } = null!;
        public long NoPrice { get; set; }
        public DateTime DateUp { get; set; }
        public string? LinkGgmap { get; set; }
        public double? AreaM2 { get; set; }
        public double? HorizontalM { get; set; }
        public bool Status { get; set; }
        public string? ImgAvar { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Regional Regional { get; set; } = null!;
        public virtual ICollection<ImageProduct> ImageProducts { get; set; }
    }
}
