using System.ComponentModel.DataAnnotations;

namespace ShopWebApp.Models.ViewModels
{
    public class UserRestore
   {
        [Required(ErrorMessage = "The user name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        public string EmailAddress { get; set; }
    }
}
