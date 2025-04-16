using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dopeClothes.Server.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public int Count { get; set; } = 1;
    }
}
