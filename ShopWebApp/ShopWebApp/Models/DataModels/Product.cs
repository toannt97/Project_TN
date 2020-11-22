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
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Create date")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Update date")]
        public DateTime? UpdateDate { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        public int SupplierID { get; set; }
        public int TotalPage { get; set; }
        public int CategoryID { get; set; }
    }
}
