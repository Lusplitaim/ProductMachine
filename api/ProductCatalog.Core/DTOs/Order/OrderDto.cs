using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Models;
using System.Text.Json;

namespace ProductCatalog.Core.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<OrderedProduct> Items { get; set; } = [];
        public int Total { get; set; }

        public static OrderDto From(OrderEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                Items = JsonSerializer.Deserialize<ICollection<OrderedProduct>>(entity.Items) ?? [],
                Total = entity.Total,
            };
        }
    }
}
