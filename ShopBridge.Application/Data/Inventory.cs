using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Application.Data
{
    internal class Inventory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
