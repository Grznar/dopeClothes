﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dopeClothes.Server.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        [ValidateNever]
        [JsonIgnore]
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
       = new List<ShoppingCartItem>();

    }
}
