using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.DTOs.ProductCategory
{
    public class ProductBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static ProductBrandDto From(ProductBrandEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
