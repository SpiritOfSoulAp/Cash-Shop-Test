using Domain;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopItemCard : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text priceText;
        [SerializeField] private Dropdown durationDropdown;
        [SerializeField] private Button buyButton;

        private ShopItem _item;
        private Action<long, string> _onBuy;

        // Bind domain item to UI
        public void Bind(ShopItem item, Action<long, string> onBuy)
        {
            _item = item;
            _onBuy = onBuy;

            nameText.text = item.Name;

            if (item.IsRental)
            {
                durationDropdown.gameObject.SetActive(true);
                durationDropdown.ClearOptions();

                var options = new List<Dropdown.OptionData>();
                foreach (var d in item.Durations)
                {
                    options.Add(
                        new Dropdown.OptionData($"{d.DisplayLabel} - {d.Price}")
                    );
                }
                durationDropdown.AddOptions(options);

                durationDropdown.value = 0;
                durationDropdown.RefreshShownValue();

                priceText.text = "";
            }
            else
            {
                durationDropdown.gameObject.SetActive(false);
                priceText.text = item.BasePrice.ToString();
            }
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyClicked);
        }
        void OnBuyClicked()
        {
            buyButton.interactable = false;
            string durationKey = "";

            if (_item.IsRental)
            {
                if (_item.Durations == null || _item.Durations.Count == 0)
                {
                    Debug.LogError("[BUY UI] No durations configured for rental item");
                    buyButton.interactable = true;
                    return;
                }

                durationKey = _item.Durations[durationDropdown.value].Seconds.ToString();
            }

            Debug.Log($"[BUY UI] itemId={_item.Id}, duration={durationKey}");
            _onBuy?.Invoke(_item.Id, durationKey);
        }

        // Re-enable button after request completes
        public void SetInteractable(bool value)
        {
            buyButton.interactable = value;
        }
    }
}
