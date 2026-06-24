using UnityEngine;
using UnityEngine.UI;

namespace TinyGarden.UI
{
    public class ParentalGate : MonoBehaviour
    {
        [SerializeField] private GameObject gatePanel;
        [SerializeField] private Text questionText;

        private void Start()
        {
            Hide();
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

        public UnityEngine.Events.UnityEvent OnGateUnlocked;

        public void OnCorrectAnswerClicked()
        {
            Debug.Log("[ParentalGate] Settings Unlocked");
            Hide();
            OnGateUnlocked?.Invoke();
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
