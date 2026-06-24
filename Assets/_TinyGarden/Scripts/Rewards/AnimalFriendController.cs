using UnityEngine;
using UnityEngine.EventSystems;

namespace TinyGarden.Rewards
{
    public class AnimalFriendController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject animalVisuals;

        public void HideAnimal()
        {
            if (animalVisuals != null)
                animalVisuals.SetActive(false);
        }

        public void ShowAnimal(bool popAnimation = false)
        {
            if (animalVisuals != null)
            {
                animalVisuals.SetActive(true);
                if (popAnimation)
                {
                    StartCoroutine(PopRoutine());
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Interactive friendly bounce
            StartCoroutine(BounceRoutine());
        }

        private System.Collections.IEnumerator PopRoutine()
        {
            Vector3 targetScale = transform.localScale;
            transform.localScale = Vector3.zero;
            
            float t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime * 3f;
                // Simple easing
                float scale = Mathf.Clamp01(t);
                transform.localScale = targetScale * scale;
                yield return null;
            }
            transform.localScale = targetScale;
            StartCoroutine(BounceRoutine());
        }

        private System.Collections.IEnumerator BounceRoutine()
        {
            Vector3 startPos = transform.localPosition;
            for (int i = 0; i < 10; i++)
            {
                float yOffset = Mathf.Sin((i / 10f) * Mathf.PI) * 20f;
                transform.localPosition = startPos + new Vector3(0, yOffset, 0);
                yield return new WaitForSeconds(0.02f);
            }
            transform.localPosition = startPos;
        }
    }
}
