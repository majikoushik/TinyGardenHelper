using UnityEngine;
using TinyGarden.MiniGames.Shared;

namespace TinyGarden.MiniGames.ShapeSort
{
    public class ShapeDropTarget : DropTargetBase
    {
        public void Setup(ShapeItemData data)
        {
            MatchId = data.shapeType.ToString();
        }

        protected override void HandleCorrectDrop(DragItemBase item)
        {
            // Optional: fade out outline or show "filled" state
        }

        protected override void HandleIncorrectDrop(DragItemBase item)
        {
            // ShapeDraggableItem handles its own wiggle.
            // Target does nothing on incorrect drop directly.
        }

        public void PulseHint()
        {
            StartCoroutine(PulseRoutine());
        }

        private System.Collections.IEnumerator PulseRoutine()
        {
            Vector3 originalScale = transform.localScale;
            for (int i = 0; i < 3; i++)
            {
                transform.localScale = originalScale * 1.15f;
                yield return new WaitForSeconds(0.15f);
                transform.localScale = originalScale;
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
