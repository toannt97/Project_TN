/*
 * 
 */
using System;

namespace ShopWebApp.Models.ViewModels
{
    public class ShippingInfoViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ShippingDate { get; set; }
    }
}
