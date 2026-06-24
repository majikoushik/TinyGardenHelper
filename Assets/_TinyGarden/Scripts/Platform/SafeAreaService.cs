using UnityEngine;
using System;

namespace TinyGarden.Platform
{
    public class SafeAreaService : MonoBehaviour
    {
        public static SafeAreaService Instance { get; private set; }

        public event Action<Rect> OnSafeAreaChanged;

        private Rect lastSafeArea = new Rect(0, 0, 0, 0);

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (Screen.safeArea != lastSafeArea)
            {
                lastSafeArea = Screen.safeArea;
                OnSafeAreaChanged?.Invoke(lastSafeArea);
            }
        }

        public Rect GetSafeArea()
        {
            return Screen.safeArea;
        }
    }
}
