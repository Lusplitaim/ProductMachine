using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.ProductCategory;

namespace ProductCatalog.Core.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int MaxQuantity { get; set; }
        public int BrandId { get; set; }
        public ProductBrandDto Brand { get; set; }

        public static ProductDto From(ProductEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                MaxQuantity = entity.MaxQuantity,
                BrandId = entity.BrandId,
                Brand = ProductBrandDto.From(entity.Brand),
            };
        }
    }
}
