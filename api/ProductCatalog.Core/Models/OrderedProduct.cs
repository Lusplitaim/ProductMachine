namespace ProductCatalog.Core.Models
{
    public class OrderedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
    }
}
