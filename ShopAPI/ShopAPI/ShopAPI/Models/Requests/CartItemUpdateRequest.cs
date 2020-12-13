using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPI.Models.Requests
{
    public class CartItemUpdateRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Status { get; set; }
        public int Quantity { get; set; }
    }
}
