using System;
using UnityEngine;

namespace TinyGarden.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event Action OnSessionStarted;
        
        // Placeholder for future session state
        public bool IsSessionActive { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeSession();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeSession()
        {
            IsSessionActive = true;
            OnSessionStarted?.Invoke();
            Debug.Log("[GameManager] Session Initialized.");
        }
    }
}
