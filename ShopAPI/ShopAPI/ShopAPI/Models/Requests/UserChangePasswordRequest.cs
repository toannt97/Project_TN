namespace ShopAPI.Models.Requests
{
    public class UserChangePasswordRequest
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
