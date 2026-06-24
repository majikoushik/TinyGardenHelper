using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TinyGarden.Audio;

namespace TinyGarden.UI
{
    [RequireComponent(typeof(Button))]
    public class ChildFriendlyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Button button;
        private Vector3 originalScale;

        [SerializeField] private float pressScale = 0.9f;
        [SerializeField] private string tapSoundCue = AudioCue.ButtonTap;

        private void Awake()
        {
            button = GetComponent<Button>();
            originalScale = transform.localScale;
            button.onClick.AddListener(OnClicked);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (button.interactable)
            {
                transform.localScale = originalScale * pressScale;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = originalScale;
        }

        private void OnClicked()
        {
            if (AudioManager.Instance != null && !string.IsNullOrEmpty(tapSoundCue))
            {
                AudioManager.Instance.PlaySfx(tapSoundCue);
            }
        }
    }
}
