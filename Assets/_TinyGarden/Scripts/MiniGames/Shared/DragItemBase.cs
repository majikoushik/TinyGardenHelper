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

        public bool IsLocked { get; protected set; }

        protected CanvasGroup canvasGroup;
        protected RectTransform rectTransform;
        protected Canvas canvas;
        protected Vector2 homePosition;

        public event Action<DragItemBase> OnDragStarted;
        public event Action<DragItemBase> OnDragEnded;

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>();
        }

        protected virtual void Start()
        {
            homePosition = rectTransform.anchoredPosition;
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (IsLocked) return;

            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.8f;

            OnDragStarted?.Invoke(this);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (IsLocked || canvas == null) return;
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if (IsLocked) return;

            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;

            OnDragEnded?.Invoke(this);
        }

        public virtual void ReturnToStart()
        {
            if (IsLocked) return;
            rectTransform.anchoredPosition = homePosition;
            HandleIncorrectFeedback();
        }

        public virtual void SetMatched(Vector3 dropPosition, bool lockItem = true)
        {
            IsMatched = true;
            IsLocked = lockItem;
            rectTransform.position = dropPosition;
            
            if (lockItem)
            {
                canvasGroup.blocksRaycasts = false; // Disable dragging once matched
            }
            
            HandleCorrectFeedback();
        }

        public virtual void Unmatch()
        {
            IsMatched = false;
            IsLocked = false;
        }

        protected abstract void HandleCorrectFeedback();
        protected abstract void HandleIncorrectFeedback();
    }
}
