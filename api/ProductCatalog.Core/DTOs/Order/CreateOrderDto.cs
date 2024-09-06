namespace ProductCatalog.Core.DTOs.Order
{
    public class CreateOrderDto
    {
        public ICollection<int> Products { get; set; } = [];
    }
}
