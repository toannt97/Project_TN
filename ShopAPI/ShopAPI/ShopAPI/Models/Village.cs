using System;
using System.Collections.Generic;

#nullable disable

namespace ShopAPI.Models
{
    public partial class Village
    {
        public string Villageid { get; set; }
        public string Name { get; set; }
        public string Wardid { get; set; }

        public virtual Ward Ward { get; set; }
    }
}
