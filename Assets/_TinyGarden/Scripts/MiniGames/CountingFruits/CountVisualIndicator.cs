using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TinyGarden.MiniGames.CountingFruits
{
    public class CountVisualIndicator : MonoBehaviour
    {
        [SerializeField] private Text targetNumberText;
        [SerializeField] private Transform dotsContainer;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Color activeDotColor = Color.green;
        [SerializeField] private Color inactiveDotColor = Color.gray;

        private List<Image> spawnedDots = new List<Image>();

        public void SetupTarget(int target)
        {
            if (targetNumberText != null)
                targetNumberText.text = target.ToString();

            // Clear old dots
            foreach (var dot in spawnedDots)
            {
                if (dot != null) Destroy(dot.gameObject);
            }
            spawnedDots.Clear();

            // Spawn new dots
            if (dotsContainer != null && dotPrefab != null)
            {
                for (int i = 0; i < target; i++)
                {
                    GameObject go = Instantiate(dotPrefab, dotsContainer);
                    Image img = go.GetComponent<Image>();
                    if (img != null)
                    {
                        img.color = inactiveDotColor;
                        spawnedDots.Add(img);
                    }
                }
            }
        }

        public void UpdateCurrentCount(int currentCount)
        {
            for (int i = 0; i < spawnedDots.Count; i++)
            {
                if (spawnedDots[i] != null)
                {
                    spawnedDots[i].color = (i < currentCount) ? activeDotColor : inactiveDotColor;
                }
            }
        }

        public void PulseHint()
        {
            StartCoroutine(PulseRoutine());
        }

        private System.Collections.IEnumerator PulseRoutine()
        {
            Vector3 origScale = targetNumberText.transform.localScale;
            targetNumberText.transform.localScale = origScale * 1.3f;
            yield return new WaitForSeconds(0.15f);
            targetNumberText.transform.localScale = origScale;
        }
    }
}
