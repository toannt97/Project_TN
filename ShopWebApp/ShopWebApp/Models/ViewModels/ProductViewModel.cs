using System.ComponentModel.DataAnnotations;

namespace ShopWebApp.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        [Display(Name = "Product name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Unit price")]
        public double UnitPrice { get; set; }
        [MaxLength(150)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Required]
        [Display(Name = "Supplier id")]
        public int SupplierId { get; set; }
        [Required]
        [Display(Name = "Supplier name")]
        public string SupplierName { get; set; }

        public string UserName { get; set; }
        public int TotalPage { get; set; }
    }
}
