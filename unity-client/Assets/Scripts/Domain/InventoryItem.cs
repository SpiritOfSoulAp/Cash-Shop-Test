using System;

namespace Domain
{
    public class InventoryItem
    {
        public long InventoryId { get; }
        public string ItemName { get; }
        public bool IsRental { get; }
        public DateTime? ExpireAtUtc { get; }

        public InventoryItem(
            long inventoryId,
            string itemName,
            bool isRental,
            DateTime? expireAtUtc)
        {
            InventoryId = inventoryId;
            ItemName = itemName;
            IsRental = isRental;
            ExpireAtUtc = expireAtUtc;
        }

        // Returns true if the item has expired based on server time.
        public bool IsExpired(DateTime serverNowUtc)
        {
            if (!IsRental || !ExpireAtUtc.HasValue)
                return false;

            return serverNowUtc >= ExpireAtUtc.Value;
        }

        // Returns remaining time until expiration.
        // Returns TimeSpan.Zero if expired or not rental.
        public TimeSpan RemainingTime(DateTime serverNowUtc)
        {
            if (!IsRental || !ExpireAtUtc.HasValue)
                return TimeSpan.Zero;

            var remaining = ExpireAtUtc.Value - serverNowUtc;
            return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
        }
    }
}
