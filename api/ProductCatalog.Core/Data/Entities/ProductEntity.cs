namespace ProductCatalog.Core.Data.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int MaxQuantity { get; set; }

        public int BrandId { get; set; }
        public ProductBrandEntity Brand { get; set; }
    }
}
