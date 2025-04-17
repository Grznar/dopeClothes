using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dopeClothes.Server.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set; }
        public string? OrderStatus { get; set; }


    }
}
