using System;
using System.Linq;
using Api.DataTransferObj;

namespace Domain.Mappers
{
    public static class DtoMapperExtensions
    {
        public static User ToDomain(this UserDto dto)
        {
            return new User(
                id: dto.id,
                cash: dto.cash
            );
        }

        public static InventoryItem ToDomain(this InventoryItemDto dto)
        {
            DateTime? expireUtc = null;
            if (!string.IsNullOrEmpty(dto.expire_at))
                expireUtc = DateTime.Parse(dto.expire_at).ToUniversalTime();

            return new InventoryItem(
                inventoryId: dto.inventory_id,
                itemName : dto.item_name, 
                isRental: expireUtc != null,
                expireAtUtc: expireUtc
            );
        }

        public static ShopItem ToDomain(this ShopItemDto dto)
        {
            var durations = dto.durations?
                .Select(d => d.ToDomain())
                .ToList();

            var isRental = durations != null && durations.Count > 0;

            return new ShopItem(
                dto.id,
                dto.code,
                dto.name,
                isRental,
                dto.base_price,
                durations
            );
        }
        public static ShopDuration ToDomain(this ShopDurationDto dto)
        {
            return new ShopDuration(
                dto.DurationKey,   // "1h", "1d", "7d"
                dto.Seconds,       // 3600, 86400, ...
                dto.Price
            );
        }

    }
}
