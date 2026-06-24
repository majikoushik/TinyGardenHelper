using UnityEngine.SceneManagement;

namespace TinyGarden.Core
{
    public static class SimpleSceneLoader
    {
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
