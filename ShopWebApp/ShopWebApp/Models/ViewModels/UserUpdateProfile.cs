using System.ComponentModel.DataAnnotations;

namespace ShopWebApp.Models.ViewModels
{
    public class UserUpdateProfile
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The password is required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "The user name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "The phone number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }
    }
}
