using System;
using System.Collections.Generic;

#nullable disable

namespace ShopAPI.Models
{
    public partial class Infor
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Seller { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Contact { get; set; }
        public string Image { get; set; }
    }
}
