using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public int ItemsInCart { get; set; }
        public int Role { get; set; }

    }
}
