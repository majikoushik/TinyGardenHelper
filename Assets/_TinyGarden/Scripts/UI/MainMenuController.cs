using UnityEngine;
using TinyGarden.Core;

namespace TinyGarden.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private ParentalGate parentalGate;

        public void OnPlayClicked()
        {
            SceneLoader.Instance.LoadScene(SceneNames.Garden);
        }

        public void OnSettingsClicked()
        {
            if (parentalGate != null)
            {
                parentalGate.Show();
            }
            else
            {
                Debug.LogWarning("Parental Gate is not assigned!");
            }
        }
    }
}
