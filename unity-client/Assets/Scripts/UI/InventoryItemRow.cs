using Domain;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace UI
{
    public class InventoryItemRow : MonoBehaviour
    {
        [SerializeField] private Text itemNameText;
        [SerializeField] private Text statusText;

        private InventoryItem _item;
        private TimeSync _time;

        // Bind inventory item to UI
        public void Bind(InventoryItem item, TimeSync timeSync, string displayName)
        {
            _item = item;
            _time = timeSync;
            itemNameText.text = displayName;
            UpdateStatus();
        }

        void Update()
        {
            if (_item == null)
                return;

            UpdateStatus();
        }

        void UpdateStatus()
        {
            var now = _time.NowServerUtc();

            if (!_item.IsRental)
            {
                statusText.text = "ถาวร";
                return;
            }

            if (_item.IsExpired(now))
            {
                statusText.text = "หมดอายุ";
                return;
            }

            var remain = _item.RemainingTime(now);
            statusText.text = FormatTime(remain);
        }

        string FormatTime(System.TimeSpan time)
        {
            if (time.TotalSeconds <= 0)
                return "00:00:00";

            int hours = (int)time.TotalHours;
            int minutes = time.Minutes;
            int seconds = time.Seconds;

            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }

    }
}
