namespace ProductCatalog.Core.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int BrandId { get; set; }
    }
}
