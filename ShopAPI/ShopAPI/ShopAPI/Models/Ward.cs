using System;
using System.Collections.Generic;

namespace ShopAPI.Models
{
    public partial class Ward
    {
        public Ward()
        {
            Village = new HashSet<Village>();
        }

        public string Wardid { get; set; }
        public string Name { get; set; }
        public string Districtid { get; set; }

        public virtual District District { get; set; }
        public virtual ICollection<Village> Village { get; set; }
    }
}
