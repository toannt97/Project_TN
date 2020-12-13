using System;
using System.Collections.Generic;

#nullable disable

namespace ShopAPI.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
        public string LinkReference { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
