using UnityEngine;
using UnityEngine.EventSystems;
using TinyGarden.MiniGames.Shared;
using TinyGarden.Audio;

namespace TinyGarden.MiniGames.CountingFruits
{
    public class FruitDraggableItem : DragItemBase
    {
        public FruitBasketDropZone CurrentBasket { get; private set; }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (IsLocked) return;

            // If we are in a basket, let's leave it
            if (CurrentBasket != null)
            {
                CurrentBasket.RemoveFruit(this);
                CurrentBasket = null;
                Unmatch();
            }

            base.OnBeginDrag(eventData);
        }

        public void SetInBasket(FruitBasketDropZone basket, Vector3 dropPos)
        {
            CurrentBasket = basket;
            // Match but DO NOT lock (allow taking it out)
            SetMatched(dropPos, false);
        }

        protected override void HandleCorrectFeedback()
        {
            // Soft plop when it hits the basket
            AudioManager.Instance?.PlaySfx("sfx_plop"); // Placeholder
        }

        protected override void HandleIncorrectFeedback()
        {
            // Just wiggle subtly on the tree if missed
            StartCoroutine(WiggleRoutine());
        }

        private System.Collections.IEnumerator WiggleRoutine()
        {
            Vector3 startPos = rectTransform.anchoredPosition;
            for (int i = 0; i < 3; i++)
            {
                rectTransform.anchoredPosition = startPos + new Vector3(Mathf.Sin(Time.time * 20f) * 10f, 0, 0);
                yield return null;
            }
            rectTransform.anchoredPosition = startPos;
        }
    }
}
