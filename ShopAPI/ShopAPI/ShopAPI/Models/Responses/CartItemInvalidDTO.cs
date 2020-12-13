using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPI.Models.Responses
{
    public class CartItemInvalidDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantityRequest { get; set; }
        public int QuantityAvailable { get; set; }
        public int Status { get; set; }
    }
}
