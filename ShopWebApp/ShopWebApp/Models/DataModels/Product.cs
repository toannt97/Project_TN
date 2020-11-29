using System;
using System.ComponentModel.DataAnnotations;

namespace ShopWebApp.Models.DataModels
{
    public class Product
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
        [MaxLength(150)]
        [Display(Name = "Information")]
        public string Information { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
        public int SupplierID { get; set; }
        [Display(Name = "Supplier: ")]
        public string SupplierName { get; set; }
        public int CategoryID { get; set; }
    }
}
