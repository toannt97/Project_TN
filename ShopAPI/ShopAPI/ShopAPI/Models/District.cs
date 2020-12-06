using System;
using System.Collections.Generic;

#nullable disable

namespace ShopAPI.Models
{
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }

        public string Districtid { get; set; }
        public string Name { get; set; }
        public string Provinceid { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
