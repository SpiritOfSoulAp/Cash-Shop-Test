using System.Collections.Generic;

namespace Domain
{
    /// Represents an item available in the shop.
    /// Contains business-friendly structure for UI and services.
    public class ShopItem
    {
        public long Id { get; }
        public string Code { get; }
        public string Name { get; }
        public bool IsRental { get; }
        public int? BasePrice { get; }
        public IReadOnlyList<ShopDuration> Durations { get; }

        public ShopItem(
            long id,
            string code,
            string name,
            bool isRental,
            int? basePrice,
            IReadOnlyList<ShopDuration> durations)
        {
            Id = id;
            Code = code;
            Name = name;
            IsRental = isRental;
            BasePrice = basePrice;
            Durations = durations;
        }
    }
}
