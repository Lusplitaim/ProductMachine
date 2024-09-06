using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.DTOs.Coin
{
    public class CoinDto
    {
        public int Nominal { get; set; }
        public int MaxQuantity { get; set; }

        public static CoinDto From(CoinEntity entity)
        {
            return new()
            {
                Nominal = entity.Nominal,
                MaxQuantity = entity.MaxQuantity,
            };
        }
    }
}
