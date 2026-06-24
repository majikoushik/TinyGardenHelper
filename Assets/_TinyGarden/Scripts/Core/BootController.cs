using System.Collections;
using UnityEngine;

namespace TinyGarden.Core
{
    public class BootController : MonoBehaviour
    {
        [SerializeField] private float bootDelaySeconds = 2.0f;

        private void Start()
        {
            // Initialize persistent systems here in the future.
            
            StartCoroutine(BootRoutine());
        }

        private IEnumerator BootRoutine()
        {
            yield return new WaitForSeconds(bootDelaySeconds);
            SceneLoader.Instance.LoadScene(SceneNames.MainMenu);
        }
    }
}
