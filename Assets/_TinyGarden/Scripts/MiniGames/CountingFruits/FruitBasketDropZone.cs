using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

namespace TinyGarden.MiniGames.CountingFruits
{
    public class FruitBasketDropZone : MonoBehaviour, IDropHandler
    {
        private List<FruitDraggableItem> fruitsInBasket = new List<FruitDraggableItem>();
        
        public event Action<int> OnBasketCountChanged;

        public void OnDrop(PointerEventData eventData)
        {
            FruitDraggableItem fruit = eventData.pointerDrag?.GetComponent<FruitDraggableItem>();
            if (fruit != null)
            {
                if (!fruitsInBasket.Contains(fruit))
                {
                    fruitsInBasket.Add(fruit);
                    
                    // Assign random slight offset within basket
                    Vector3 dropOffset = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-20f, 20f), 0);
                    fruit.SetInBasket(this, transform.position + dropOffset);

                    OnBasketCountChanged?.Invoke(fruitsInBasket.Count);
                    PulseBasket();
                }
            }
        }

        public void RemoveFruit(FruitDraggableItem fruit)
        {
            if (fruitsInBasket.Contains(fruit))
            {
                fruitsInBasket.Remove(fruit);
                OnBasketCountChanged?.Invoke(fruitsInBasket.Count);
            }
        }

        public int GetCount()
        {
            return fruitsInBasket.Count;
        }

        public void ClearBasket()
        {
            foreach (var fruit in fruitsInBasket)
            {
                fruit.ReturnToStart();
                fruit.Unmatch();
            }
            fruitsInBasket.Clear();
            OnBasketCountChanged?.Invoke(0);
        }

        public void PulseBasket()
        {
            StartCoroutine(PulseRoutine());
        }

        private System.Collections.IEnumerator PulseRoutine()
        {
            Vector3 originalScale = transform.localScale;
            transform.localScale = originalScale * 1.05f;
            yield return new WaitForSeconds(0.1f);
            transform.localScale = originalScale * 0.95f;
            yield return new WaitForSeconds(0.1f);
            transform.localScale = originalScale;
        }
    }
}
