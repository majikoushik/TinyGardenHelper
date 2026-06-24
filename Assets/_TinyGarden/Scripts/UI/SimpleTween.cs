using System.Collections;
using UnityEngine;

namespace TinyGarden.UI
{
    public static class SimpleTween
    {
        public static IEnumerator Scale(Transform target, Vector3 fromScale, Vector3 toScale, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                
                // Simple ease out
                t = 1f - Mathf.Pow(1f - t, 3f);
                
                if (target != null)
                {
                    target.localScale = Vector3.Lerp(fromScale, toScale, t);
                }
                yield return null;
            }

            if (target != null)
            {
                target.localScale = toScale;
            }
        }
    }
}
