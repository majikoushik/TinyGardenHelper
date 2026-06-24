using UnityEngine;
using TinyGarden.Core;

namespace TinyGarden.Garden
{
    public class GardenSceneController : MonoBehaviour
    {
        public void OnHomeClicked()
        {
            SceneLoader.Instance.LoadScene(SceneNames.MainMenu);
        }
        
        // Developer Tool for Testing
        public void OnResetProgressClicked()
        {
            if (GameManager.Instance != null && GameManager.Instance.SaveSystem != null)
            {
                GameManager.Instance.SaveSystem.ResetProgress();
                
                var presenter = FindObjectOfType<GardenProgressPresenter>();
                if (presenter != null)
                {
                    presenter.EvaluateGardenState();
                }
                
                Debug.Log("[Developer] Progress Reset.");
            }
        }
    }
}
