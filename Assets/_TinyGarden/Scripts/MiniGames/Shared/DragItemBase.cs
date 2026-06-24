using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TinyGarden.MiniGames.Shared
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class DragItemBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public string MatchId;
        public bool IsMatched { get; private set; }

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Canvas canvas;
        private Vector2 originalPosition;

        public event Action<DragItemBase> OnDragStarted;
        public event Action<DragItemBase> OnDragEnded;

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (IsMatched) return;

            originalPosition = rectTransform.anchoredPosition;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.8f;

            OnDragStarted?.Invoke(this);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (IsMatched || canvas == null) return;
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if (IsMatched) return;

            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;

            OnDragEnded?.Invoke(this);
        }

        public virtual void ReturnToStart()
        {
            if (IsMatched) return;
            rectTransform.anchoredPosition = originalPosition;
            HandleIncorrectFeedback();
        }

        public virtual void SetMatched(Vector3 dropPosition)
        {
            IsMatched = true;
            rectTransform.position = dropPosition;
            canvasGroup.blocksRaycasts = false; // Disable dragging once matched
            HandleCorrectFeedback();
        }

        protected abstract void HandleCorrectFeedback();
        protected abstract void HandleIncorrectFeedback();
    }
}
