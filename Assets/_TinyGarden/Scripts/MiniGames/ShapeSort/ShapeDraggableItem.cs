using UnityEngine;
using TinyGarden.MiniGames.Shared;
using TinyGarden.Audio;

namespace TinyGarden.MiniGames.ShapeSort
{
    public class ShapeDraggableItem : DragItemBase
    {
        public ShapeType ShapeType { get; private set; }
        
        private int failedAttempts = 0;
        private ShapeSortController controller;

        public void Setup(ShapeItemData data, ShapeSortController controllerRef)
        {
            ShapeType = data.shapeType;
            MatchId = data.shapeType.ToString();
            controller = controllerRef;
            
            // Assume UI setup is handled externally (colors, sprites)
        }

        protected override void HandleCorrectFeedback()
        {
            failedAttempts = 0;
            AudioManager.Instance?.PlaySfx("sfx_shape_correct");

            // Play specific micro-animation based on shape
            switch (ShapeType)
            {
                case ShapeType.Circle:
                    StartCoroutine(RollRoutine());
                    break;
                case ShapeType.Square:
                    StartCoroutine(HopRoutine());
                    break;
                case ShapeType.Triangle:
                    StartCoroutine(WiggleRoutine());
                    break;
            }
        }

        protected override void HandleIncorrectFeedback()
        {
            failedAttempts++;

            // Wiggle gently to show it was wrong
            StartCoroutine(WiggleRoutine());

            if (failedAttempts >= 2 && controller != null)
            {
                // Trigger progressive hint system
                controller.TriggerHintForShape(MatchId);
            }
        }

        private System.Collections.IEnumerator RollRoutine()
        {
            for (int i = 0; i < 15; i++)
            {
                transform.Rotate(0, 0, -24f);
                yield return new WaitForSeconds(0.02f);
            }
            transform.rotation = Quaternion.identity;
        }

        private System.Collections.IEnumerator HopRoutine()
        {
            Vector3 startPos = rectTransform.anchoredPosition;
            for (int i = 0; i < 10; i++)
            {
                float yOffset = Mathf.Sin((i / 10f) * Mathf.PI) * 30f;
                rectTransform.anchoredPosition = startPos + new Vector3(0, yOffset, 0);
                yield return new WaitForSeconds(0.02f);
            }
            rectTransform.anchoredPosition = startPos;
        }

        private System.Collections.IEnumerator WiggleRoutine()
        {
            Vector3 startPos = rectTransform.anchoredPosition;
            for (int i = 0; i < 4; i++)
            {
                float xOffset = (i % 2 == 0 ? 10f : -10f);
                rectTransform.anchoredPosition = startPos + new Vector3(xOffset, 0, 0);
                yield return new WaitForSeconds(0.05f);
            }
            rectTransform.anchoredPosition = startPos;
        }
    }
}
