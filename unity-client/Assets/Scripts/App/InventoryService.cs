using Core;
using Domain;
using Domain.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DataTransferObj;
using System.Diagnostics;

namespace App
{
    public class InventoryService
    {
        private readonly ApiClient _api;
        private readonly GameState _state;

        public InventoryService(ApiClient api, GameState state)
        {
            _api = api;
            _state = state;
        }

        // Fetch inventory from server (only valid items should be returned)
        public async Task<Result<bool>> FetchInventoryAsync()
        {
            var result = await _api.Get<List<InventoryItemDto>>($"/api/inventory?user_id={_state.User.Id}");

            if (!result.IsOk)
                return Result<bool>.Fail(result.Error);

            var items = result.Value
                .Select(dto => dto.ToDomain())
                .ToList();

            _state.SetInventory(items);
            return Result<bool>.Ok(true);
        }

    }
}
