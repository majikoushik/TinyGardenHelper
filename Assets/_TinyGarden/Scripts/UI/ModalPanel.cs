using UnityEngine;
using UnityEngine.UI;
using System;

namespace TinyGarden.UI
{
    public class ModalPanel : MonoBehaviour
    {
        [SerializeField] private Text titleText;
        [SerializeField] private Text messageText;
        [SerializeField] private GameObject panelRoot;
        
        private Action onConfirmAction;

        private void Awake()
        {
            Hide();
        }

        public void Show(string title, string message, Action onConfirm = null)
        {
            if (titleText != null) titleText.text = title;
            if (messageText != null) messageText.text = message;
            
            onConfirmAction = onConfirm;
            
            if (panelRoot != null) panelRoot.SetActive(true);
        }

        public void OnConfirmClicked()
        {
            Hide();
            onConfirmAction?.Invoke();
        }

        public void Hide()
        {
            if (panelRoot != null) panelRoot.SetActive(false);
            onConfirmAction = null;
        }
    }
}
