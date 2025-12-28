using App;
using Core;
using UnityEngine;
using UI;

namespace Bootstrap
{
    public class GameInstaller : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private ApiConfig apiConfig;

        [Header("UI Screens")]
        [SerializeField] private ShopScreen shopScreen;
        [SerializeField] private InventoryScreen inventoryScreen;

        void Awake()
        {
            // Core
            var timeSync = new TimeSync();
            var gameState = new GameState();

            // Api
            var apiClient = new ApiClient(apiConfig);

            // Services
            var userService = new UserService(apiClient, gameState, timeSync);
            var inventoryService = new InventoryService(apiClient, gameState);
            var shopService = new ShopService(apiClient, gameState, timeSync, inventoryService);

            // Inject dependencies into UI
            shopScreen.UserService = userService;
            shopScreen.InventoryService = inventoryService;
            shopScreen.ShopService = shopService;
            shopScreen.GameState = gameState;

            inventoryScreen.GameState = gameState;
            inventoryScreen.TimeSync = timeSync;
        }
    }
}
