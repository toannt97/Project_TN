using System.ComponentModel.DataAnnotations;

namespace ShopWebApp.Models.DataModels
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Brand name")]
        public string Name { get; set; }
    }
}
