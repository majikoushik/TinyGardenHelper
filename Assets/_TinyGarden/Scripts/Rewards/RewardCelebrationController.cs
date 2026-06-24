using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TinyGarden.Audio;

namespace TinyGarden.Rewards
{
    public class RewardCelebrationController : MonoBehaviour
    {
        [SerializeField] private GameObject celebrationPanel;
        [SerializeField] private Text celebrationText;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Start()
        {
            if (celebrationPanel != null)
                celebrationPanel.SetActive(false);
        }

        public void PlayAnimalUnlockSequence(string animalRewardId, string stickerRewardId, Action onComplete)
        {
            StartCoroutine(CelebrationRoutine(animalRewardId, stickerRewardId, onComplete));
        }

        private IEnumerator CelebrationRoutine(string animalRewardId, string stickerRewardId, Action onComplete)
        {
            if (celebrationPanel != null)
            {
                celebrationPanel.SetActive(true);
                if (canvasGroup != null) canvasGroup.alpha = 0;
            }

            if (celebrationText != null)
            {
                celebrationText.text = "A new garden friend!";
            }

            // Fade in
            float t = 0;
            while (t < 1f && canvasGroup != null)
            {
                t += Time.deltaTime * 2f;
                canvasGroup.alpha = t;
                yield return null;
            }

            // Play sparkles and sounds
            AudioManager.Instance?.PlaySfx("sfx_celebration");

            // Grant rewards while celebration is obscuring the screen
            RewardSystem.Instance.GrantReward(animalRewardId);
            RewardSystem.Instance.GrantReward(stickerRewardId);

            // Wait a moment for child to read/hear
            yield return new WaitForSeconds(2.5f);

            // Notify to show animal before we fade out
            onComplete?.Invoke();

            // Fade out
            t = 1f;
            while (t > 0 && canvasGroup != null)
            {
                t -= Time.deltaTime * 2f;
                canvasGroup.alpha = t;
                yield return null;
            }

            if (celebrationPanel != null)
                celebrationPanel.SetActive(false);
        }
    }
}
