namespace ProductCatalog.Core.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int BrandId { get; set; }
    }
}
