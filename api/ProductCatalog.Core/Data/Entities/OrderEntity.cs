using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Items { get; set; }
        public int Total { get; set; }
    }
}
