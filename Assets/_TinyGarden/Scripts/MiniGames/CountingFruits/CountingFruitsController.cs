using UnityEngine;
using System.Collections;
using TinyGarden.Core;
using TinyGarden.MiniGames.Shared;
using TinyGarden.Audio;

namespace TinyGarden.MiniGames.CountingFruits
{
    public class CountingFruitsController : MiniGameBase
    {
        [SerializeField] private CountingFruitsDefinition definition;
        [SerializeField] private CountVisualIndicator visualIndicator;
        [SerializeField] private FruitBasketDropZone basket;
        [SerializeField] private GameObject celebrationPanel;

        private int currentRoundIndex = 0;
        private bool isProcessingRoundComplete = false;
        private Coroutine completionCoroutine;

        protected override void Start()
        {
            base.Start();

            if (celebrationPanel != null)
                celebrationPanel.SetActive(false);

            if (basket != null)
                basket.OnBasketCountChanged += HandleBasketCountChanged;

            StartRound(0);
        }

        private void StartRound(int index)
        {
            currentRoundIndex = index;
            isProcessingRoundComplete = false;

            if (definition == null || definition.rounds == null || index >= definition.rounds.Length)
            {
                OnGameComplete();
                return;
            }

            var roundData = definition.rounds[index];
            
            // Setup Visuals
            if (visualIndicator != null)
            {
                visualIndicator.SetupTarget(roundData.targetCount);
                visualIndicator.UpdateCurrentCount(0);
            }

            // Clear basket from previous round
            if (basket != null)
                basket.ClearBasket();

            // Play voice prompt
            if (!string.IsNullOrEmpty(roundData.voicePromptId))
            {
                AudioManager.Instance?.PlayVoice(roundData.voicePromptId);
            }
        }

        private void HandleBasketCountChanged(int currentCount)
        {
            if (definition == null) return;

            var roundData = definition.rounds[currentRoundIndex];
            int target = roundData.targetCount;

            if (visualIndicator != null)
                visualIndicator.UpdateCurrentCount(currentCount);

            if (currentCount == target)
            {
                if (!isProcessingRoundComplete)
                {
                    isProcessingRoundComplete = true;
                    completionCoroutine = StartCoroutine(CompleteRoundRoutine());
                }
            }
            else
            {
                // Count changed away from target! Cancel auto-advance if pending.
                if (isProcessingRoundComplete)
                {
                    if (completionCoroutine != null) StopCoroutine(completionCoroutine);
                    isProcessingRoundComplete = false;
                }

                if (currentCount > target)
                {
                    // Too many fruits! Gentle feedback.
                    if (visualIndicator != null) visualIndicator.PulseHint();
                    if (basket != null) basket.PulseBasket();
                    
                    // Play soft boing or gentle hint sound
                    AudioManager.Instance?.PlaySfx("sfx_hint_boing"); // Placeholder
                }
            }
        }

        private IEnumerator CompleteRoundRoutine()
        {
            
            // Brief pause so the child sees the final fruit land and the visual indicator light up
            yield return new WaitForSeconds(1.0f);

            // Play a small success chime for the round
            AudioManager.Instance?.PlaySfx("sfx_round_success"); // Placeholder

            yield return new WaitForSeconds(0.5f);

            currentRoundIndex++;
            StartRound(currentRoundIndex);
        }

        private void OnGameComplete()
        {
            Debug.Log("[CountingFruits] All rounds complete! Showing celebration.");
            
            if (celebrationPanel != null)
            {
                celebrationPanel.SetActive(true);
            }

            // Save Progress
            if (GameManager.Instance != null && GameManager.Instance.SaveSystem != null)
            {
                var id = definition != null ? definition.activityId : ActivityId.CountingFruits;
                GameManager.Instance.SaveSystem.MarkActivityCompleted(id);
            }

            AudioManager.Instance?.PlaySfx("sfx_celebration"); // Placeholder
        }

        public void ReturnToGarden()
        {
            SceneLoader.Instance.LoadScene(SceneNames.Garden);
        }

        private void OnDestroy()
        {
            if (basket != null)
                basket.OnBasketCountChanged -= HandleBasketCountChanged;
        }
    }
}
