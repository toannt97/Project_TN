﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Models.DTO
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
        public string ProductInformation { get; set; }
        public decimal TotalPrice => Convert.ToDecimal(Quantity) * UnitPrice;

    }
}
