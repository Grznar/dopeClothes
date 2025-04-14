using System.ComponentModel.DataAnnotations;

namespace dopeClothes.Server.Models.VMs
{
    public class ProductVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double SalePrice { get; set; }
        public string? Category { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
