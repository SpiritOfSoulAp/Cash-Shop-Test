using System.Threading.Tasks;
using Core;
using Domain.Mappers;
using Api.DataTransferObj;

namespace App
{
    public class UserService
    {
        private readonly ApiClient _api;
        private readonly GameState _state;
        private readonly TimeSync _time;

        public UserService(ApiClient api, GameState state, TimeSync time)
        {
            _api = api;
            _state = state;
            _time = time;
        }

        // Load user from server and sync server time
        public async Task<Result<bool>> FetchUserAsync()
        {
            // Fetch user data hardcoded id=1 is fixed for demo purposes
            var result = await _api.Get<UserDto>("/api/user/1");
            if (!result.IsOk)
                return Result<bool>.Fail(result.Error);

            _state.SetUser(result.Value.ToDomain());

            return Result<bool>.Ok(true);
        }
    }
}
