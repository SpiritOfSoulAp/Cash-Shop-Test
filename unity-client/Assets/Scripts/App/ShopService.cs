using Core;
using Domain;
using Domain.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DataTransferObj;

namespace App
{
    public class ShopService
    {
        private readonly ApiClient _api;
        private readonly GameState _state;
        private readonly TimeSync _time;
        private readonly InventoryService _inventory;

        public ShopService(
            ApiClient api,
            GameState state,
            TimeSync time,
            InventoryService inventory)
        {
            _api = api;
            _state = state;
            _time = time;
            _inventory = inventory;
        }

        // Load shop items
        public async Task<Result<bool>> FetchShopItemsAsync()
        {
            var result = await _api.Get<List<ShopItemDto>>("/api/shop/items");

            if (!result.IsOk)
                return Result<bool>.Fail(result.Error);

            var items = new List<ShopItem>();

            foreach (var dto in result.Value)
            {
                items.Add(dto.ToDomain());
            }

            _state.SetShopItems(items);
            return Result<bool>.Ok(true);
        }

        // Buy item 
        public async Task<Result<bool>> BuyItemAsync(long itemId, string durationKey = null)
        {
            if (_state.User == null)
            {
                return Result<bool>.Fail(new ApiError
                {
                    Code = "USER_NOT_LOADED",
                    Message = "User not loaded"
                });
            }

            var requestId = System.Guid.NewGuid().ToString();

            var request = new BuyRequestDto
            {
                user_id = _state.User.Id,
                item_id = itemId,
                duration_key = durationKey,
                request_id = requestId
            };

            var result = await _api.Post<BuyResponseDto>(
                "/api/shop/buy",
                request,
                requestId
            );

            if (!result.IsOk)
            {
                if (result.Error.Code == "DUPLICATE_REQUEST")
                {
                    await _inventory.FetchInventoryAsync();
                    return Result<bool>.Ok(true);
                }

                return Result<bool>.Fail(result.Error);
            }

            _state.UpdateUserCash(result.Value.cash_after);
            await _inventory.FetchInventoryAsync();

            return Result<bool>.Ok(true);
        }
    }
}





