using System;
using UnityEngine;
using TinyGarden.Audio;

namespace TinyGarden.MiniGames.Shared
{
    public abstract class MiniGameBase : MonoBehaviour, IMiniGame
    {
        public event Action<MiniGameResult> OnGameCompleted;
        
        protected int currentAttempts = 0;
        
        [SerializeField] protected ActivityDefinition activityDefinition;

        public ActivityId GetActivityId()
        {
            return activityDefinition != null ? activityDefinition.activityId : ActivityId.None;
        }

        public virtual void StartGame()
        {
            currentAttempts = 0;
            Debug.Log($"[MiniGame] Started {GetActivityId()}");
        }

        public virtual void EndGame(bool success)
        {
            var result = new MiniGameResult
            {
                ActivityId = GetActivityId(),
                IsSuccess = success,
                Attempts = currentAttempts
            };
            
            if (success && AudioManager.Instance != null && activityDefinition != null)
            {
                if (!string.IsNullOrEmpty(activityDefinition.successVoiceLineId))
                {
                    AudioManager.Instance.PlayVoice(activityDefinition.successVoiceLineId);
                }
            }

            OnGameCompleted?.Invoke(result);
            Debug.Log($"[MiniGame] Ended {GetActivityId()} with success={success}");
        }

        protected virtual void HandleIncorrectAttempt()
        {
            currentAttempts++;
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayVoice(AudioCue.TryAgain);
            }
        }
    }
}
