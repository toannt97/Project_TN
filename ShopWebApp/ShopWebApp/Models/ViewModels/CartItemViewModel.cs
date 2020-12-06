using ShopWebApp.Models.DataModels;
using System;

namespace ShopWebApp.Models.ViewModels
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductInformation { get; set; }
        public int ProductQuantityAvailable { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string SupplierName { get; set; }
        public decimal TotalPrice => Convert.ToDecimal(Quantity) * UnitPrice;
    }
}
