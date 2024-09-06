namespace ProductCatalog.Core.Models
{
    public class ProductFilters
    {
        public ICollection<int> Brands { get; set; } = [];
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
    }
}
