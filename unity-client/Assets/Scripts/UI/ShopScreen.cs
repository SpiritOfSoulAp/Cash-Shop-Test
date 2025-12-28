using System.Collections.Generic;
using App;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopScreen : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Text cashText;
        [SerializeField] private Transform itemContainer;
        [SerializeField] private ShopItemCard itemPrefab;
        [SerializeField] private PopupDialog popup;

        [Header("Services")]
        public UserService UserService;
        public ShopService ShopService;
        public InventoryService InventoryService;
        public GameState GameState;

        private readonly List<ShopItemCard> _cards = new();

        async void Start()
        {
            GameState.OnUserChanged += RefreshCash;
            GameState.OnShopChanged += RefreshShop;

            var user = await UserService.FetchUserAsync();
            if (!user.IsOk)
            {
                popup.Show("Failed to load user");
                return;
            }

            var shop = await ShopService.FetchShopItemsAsync();
            if (!shop.IsOk)
            {
                popup.Show("Failed to load shop");
                return;
            }

            await InventoryService.FetchInventoryAsync();
        }

        void RefreshCash()
        {
            if (GameState.User == null) return;

            cashText.text = GameState.User.Cash.ToString();
        }


        void RefreshShop()
        {
            ClearItems();

            foreach (var item in GameState.ShopItems)
            {
                var card = Instantiate(itemPrefab, itemContainer);
                _cards.Add(card);

                card.Bind(item, async (itemId, durationKey) =>
                {
                    Debug.Log($"[BUY UI] itemId={item.Id}, code={item.Code}, duration={durationKey}");

                    var result = await ShopService.BuyItemAsync(item.Id, durationKey);

                    if (!result.IsOk)
                    {
                        popup.Show(result.Error.Message);
                        card.SetInteractable(true);
                        return;
                    }

                    popup.Show("Purchase successful");
                    card.SetInteractable(true);
                });
            }
        }
        void ClearItems()
        {
            foreach (var c in _cards)
                Destroy(c.gameObject);

            _cards.Clear();
        }

        void OnDestroy()
        {
            GameState.OnUserChanged -= RefreshCash;
            GameState.OnShopChanged -= RefreshShop;
        }
    }
}
