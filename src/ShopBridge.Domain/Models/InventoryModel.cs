using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Domain.Models
{
    public class InventoryModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        public int QuantityInStock { get; set; }
    }
}
