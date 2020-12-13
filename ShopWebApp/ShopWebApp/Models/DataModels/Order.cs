using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Models.DataModels
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int Status { get; set; }
    }
}
