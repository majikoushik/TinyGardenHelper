using UnityEngine;
using UnityEngine.UI;
using TinyGarden.MiniGames.Shared;

namespace TinyGarden.MiniGames.ColorMatch
{
    public class ColorDropTarget : DropTargetBase
    {
        [SerializeField] private Image targetImage;
        private int incorrectAttempts = 0;

        public void Setup(ColorMatchItemData data)
        {
            MatchId = data.colorId;
            if (targetImage != null)
            {
                if (data.targetSprite != null)
                {
                    targetImage.sprite = data.targetSprite;
                }
                // Tint target slightly transparent to show it's empty
                var c = data.colorValue;
                c.a = 0.5f;
                targetImage.color = c;
            }
        }

        protected override void HandleCorrectDrop(DragItemBase item)
        {
            // Restore full opacity on success
            if (targetImage != null)
            {
                var c = targetImage.color;
                c.a = 1f;
                targetImage.color = c;
            }
        }

        protected override void HandleIncorrectDrop(DragItemBase item)
        {
            incorrectAttempts++;
            StartCoroutine(HintPulseRoutine());

            // If incorrect multiple times, maybe trigger a voice hint in a fuller implementation
        }

        private System.Collections.IEnumerator HintPulseRoutine()
        {
            Vector3 originalScale = transform.localScale;
            // Pulse to indicate "I am the target you missed!" if they dropped something wrong on us
            // Or just wiggle
            for (int i = 0; i < 3; i++)
            {
                transform.localScale = originalScale * 1.1f;
                yield return new WaitForSeconds(0.1f);
                transform.localScale = originalScale;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
