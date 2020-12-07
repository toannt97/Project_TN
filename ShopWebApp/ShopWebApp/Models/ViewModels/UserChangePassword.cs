using System.ComponentModel.DataAnnotations;

namespace ShopWebApp.Models.ViewModels
{
    public class UserChangePassword
    {
        [Required(ErrorMessage = "The password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "The password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "The password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string PasswordComfirmation { get; set; }
    }
}
