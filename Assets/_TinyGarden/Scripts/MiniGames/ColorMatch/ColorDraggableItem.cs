using UnityEngine;
using UnityEngine.UI;
using TinyGarden.MiniGames.Shared;
using TinyGarden.Audio;

namespace TinyGarden.MiniGames.ColorMatch
{
    public class ColorDraggableItem : DragItemBase
    {
        [SerializeField] private Image itemImage;

        public void Setup(ColorMatchItemData data)
        {
            MatchId = data.colorId;
            if (itemImage != null)
            {
                if (data.itemSprite != null)
                {
                    itemImage.sprite = data.itemSprite;
                }
                itemImage.color = data.colorValue;
            }
        }

        protected override void HandleCorrectFeedback()
        {
            // Play success sound
            AudioManager.Instance?.PlaySfx(AudioCue.SuccessSparkle);

            // Simple scale bounce animation
            StartCoroutine(PulseRoutine(1.2f));
        }

        protected override void HandleIncorrectFeedback()
        {
            // Gentle try again, maybe a subtle wiggle or pop
            StartCoroutine(PulseRoutine(0.9f));
        }

        private System.Collections.IEnumerator PulseRoutine(float targetScale)
        {
            Vector3 originalScale = transform.localScale;
            transform.localScale = originalScale * targetScale;
            yield return new WaitForSeconds(0.15f);
            transform.localScale = originalScale;
        }
    }
}
