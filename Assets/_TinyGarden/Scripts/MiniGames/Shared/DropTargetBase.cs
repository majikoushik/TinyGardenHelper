using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TinyGarden.MiniGames.Shared
{
    public abstract class DropTargetBase : MonoBehaviour, IDropHandler
    {
        public string MatchId;
        public bool HasItem { get; private set; }

        public event Action<DropTargetBase, DragItemBase, bool> OnItemDropped;

        public virtual void OnDrop(PointerEventData eventData)
        {
            if (HasItem) return;

            DragItemBase draggedItem = eventData.pointerDrag?.GetComponent<DragItemBase>();
            if (draggedItem != null)
            {
                bool isCorrect = CheckMatch(draggedItem);
                
                if (isCorrect)
                {
                    HasItem = true;
                    draggedItem.SetMatched(transform.position);
                    HandleCorrectDrop(draggedItem);
                }
                else
                {
                    draggedItem.ReturnToStart();
                    HandleIncorrectDrop(draggedItem);
                }

                OnItemDropped?.Invoke(this, draggedItem, isCorrect);
            }
        }

        protected virtual bool CheckMatch(DragItemBase item)
        {
            return item.MatchId == this.MatchId;
        }

        protected abstract void HandleCorrectDrop(DragItemBase item);
        protected abstract void HandleIncorrectDrop(DragItemBase item);
    }
}
