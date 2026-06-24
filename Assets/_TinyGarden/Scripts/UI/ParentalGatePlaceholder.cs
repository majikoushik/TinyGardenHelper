using UnityEngine;
using UnityEngine.UI;

namespace TinyGarden.UI
{
    public class ParentalGatePlaceholder : MonoBehaviour
    {
        [SerializeField] private GameObject gatePanel;
        [SerializeField] private Text questionText;

        private void Start()
        {
            if (gatePanel != null)
            {
                gatePanel.SetActive(false);
            }
        }

        public void Show()
        {
            if (gatePanel != null)
            {
                gatePanel.SetActive(true);
                if (questionText != null)
                {
                    questionText.text = "For grown-ups: What is 12 x 11?";
                }
            }
        }

        public void OnCorrectAnswerClicked()
        {
            Debug.Log("Settings Unlocked (Placeholder)");
            Hide();
            // In the future, this will open the actual settings menu.
        }

        public void OnIncorrectAnswerClicked()
        {
            if (questionText != null)
            {
                questionText.text = "Try again! What is 12 x 11?";
            }
        }

        public void Hide()
        {
            if (gatePanel != null)
            {
                gatePanel.SetActive(false);
            }
        }
    }
}
