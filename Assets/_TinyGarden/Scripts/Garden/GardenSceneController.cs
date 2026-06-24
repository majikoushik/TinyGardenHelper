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
    }
}
