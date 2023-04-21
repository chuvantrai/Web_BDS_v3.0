using System;
using System.Collections.Generic;

namespace WebBDS.Models
{
    public partial class UserRequest
    {
        public int RequestId { get; set; }
        public int UseId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime DateRequest { get; set; }
        public bool Status { get; set; }

        public virtual User Use { get; set; } = null!;
    }
}
