using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopupDialog : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Text messageText;
        [SerializeField] private Button closeButton;

        private System.Action _onClose;
        private void Awake()
        {
            closeButton.onClick.AddListener(Hide);
        }
        public void Show(string message, System.Action onClose = null)
        {
            messageText.text = message;
            _onClose = onClose;
            root.SetActive(true);
        }

        public void Hide()
        {
            root.SetActive(false);
            _onClose?.Invoke();
            _onClose = null;
        }
    }
}
