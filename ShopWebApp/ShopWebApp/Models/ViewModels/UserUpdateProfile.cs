using System.ComponentModel.DataAnnotations;

namespace ShopWebApp.Models.ViewModels
{
    public class UserUpdateProfile
    {
        public int Id { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
