using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dopeClothes.Server.Models
{
    public class WishListItem
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("WhishList")]
        public int WishListId { get; set; }
        [ValidateNever]
        public WhishList WhishList { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
    }
}
