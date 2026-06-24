using UnityEngine;

namespace TinyGarden.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private ModalPanel defaultModal;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowModal(string title, string message, System.Action onConfirm = null)
        {
            if (defaultModal != null)
            {
                defaultModal.Show(title, message, onConfirm);
            }
            else
            {
                Debug.LogWarning($"[UIManager] No default modal set. Message: {title} - {message}");
                onConfirm?.Invoke();
            }
        }
    }
}
