using UnityEngine;
using UnityEngine.EventSystems;
using TinyGarden.Core;

namespace TinyGarden.UI
{
    public class ButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float pressScale = 0.9f;
        [SerializeField] private float duration = 0.15f;
        
        private Vector3 originalScale;
        private Coroutine currentTween;

        private void Start()
        {
            originalScale = transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsSensorySafeModeEnabled()) return;

            if (currentTween != null) StopCoroutine(currentTween);
            currentTween = StartCoroutine(SimpleTween.Scale(transform, transform.localScale, originalScale * pressScale, duration));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (IsSensorySafeModeEnabled()) return;

            if (currentTween != null) StopCoroutine(currentTween);
            currentTween = StartCoroutine(SimpleTween.Scale(transform, transform.localScale, originalScale, duration));
        }

        private bool IsSensorySafeModeEnabled()
        {
            if (GameManager.Instance != null && GameManager.Instance.SaveSystem != null)
            {
                var data = GameManager.Instance.SaveSystem.CurrentData;
                if (data != null && data.Settings != null)
                {
                    return data.Settings.SensorySafeModeEnabled;
                }
            }
            return false;
        }
    }
}
