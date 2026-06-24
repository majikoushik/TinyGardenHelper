using UnityEngine;
using TinyGarden.Core;

namespace TinyGarden.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private ParentalGatePlaceholder parentalGate;

        public void OnPlayClicked()
        {
            SimpleSceneLoader.LoadScene(SceneNames.Garden);
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
