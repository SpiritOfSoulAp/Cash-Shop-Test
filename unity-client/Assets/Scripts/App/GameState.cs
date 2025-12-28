using System;
using System.Linq;
using System.Collections.Generic;
using Domain;

namespace App
{
    public class GameState
    {
        public User User { get; private set; }
        public IReadOnlyList<ShopItem> ShopItems => _shopItems;
        public IReadOnlyList<InventoryItem> Inventory => _inventory;

        public event Action OnUserChanged;
        public event Action OnShopChanged;
        public event Action OnInventoryChanged;

        private List<ShopItem> _shopItems = new();
        private List<InventoryItem> _inventory = new();

        public void SetUser(User user)
        {
            User = user;
            OnUserChanged?.Invoke();
        }
        public void UpdateUserCash(int newCash)
        {
            User.UpdateCash(newCash);
            OnUserChanged?.Invoke();
        }


        public void SetShopItems(List<ShopItem> items)
        {
            _shopItems = items;
            OnShopChanged?.Invoke();
        }
        public void SetInventory(List<InventoryItem> items)
        {
            _inventory = items;
            OnInventoryChanged?.Invoke();
        }

        public ShopItem GetShopItemById(long shopItemId)
        {
            return _shopItems.FirstOrDefault(i => i.Id == shopItemId);
        }

        public string GetShopItemName(long shopItemId)
        {
            var item = GetShopItemById(shopItemId);
            return item != null ? item.Name : $"Item #{shopItemId}";
        }
    }
}
