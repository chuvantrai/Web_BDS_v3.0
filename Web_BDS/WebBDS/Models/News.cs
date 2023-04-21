using System;
using System.Collections.Generic;

namespace WebBDS.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime DateUp { get; set; }
        public string? ImgAvar { get; set; }
    }
}
