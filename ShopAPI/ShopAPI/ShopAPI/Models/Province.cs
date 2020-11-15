using System;
using System.Collections.Generic;

namespace ShopAPI.Models
{
    public partial class Province
    {
        public Province()
        {
            District = new HashSet<District>();
        }

        public string Provinceid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<District> District { get; set; }
    }
}
