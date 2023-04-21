using System;
using System.Collections.Generic;

namespace WebBDS.Models
{
    public partial class ImageProduct
    {
        public int ImgId { get; set; }
        public int ProductId { get; set; }
        public string ImgName { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
