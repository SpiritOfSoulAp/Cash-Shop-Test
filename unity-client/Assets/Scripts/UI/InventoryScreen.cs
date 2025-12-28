using App;
using Core;
using Domain;
using UnityEngine;

namespace UI
{
    public class InventoryScreen : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Transform itemContainer;
        [SerializeField] private InventoryItemRow itemPrefab;

        [Header("Dependencies")]
        public GameState GameState;
        public TimeSync TimeSync;

        void Start()
        {
            GameState.OnInventoryChanged += RefreshInventory;
            RefreshInventory();
        }

        void RefreshInventory()
        {
            ClearItems();

            foreach (var item in GameState.Inventory)
            {
                var row = Instantiate(itemPrefab, itemContainer);
                var displayName = item.ItemName; 

                row.Bind(item, TimeSync, displayName);
            }
        }

        void ClearItems()
        {
            foreach (Transform child in itemContainer)
                Destroy(child.gameObject);
        }

        void OnDestroy()
        {
            GameState.OnInventoryChanged -= RefreshInventory;
        }
    }
}
