using System;
using System.Collections.Generic;

#nullable disable

namespace ShopAPI.Models
{
    public partial class User
    {
        public User()
        {
            Histories = new HashSet<History>();
            Orders = new HashSet<Order>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
