using UnityEngine;
using UnityEngine.UI;
using TinyGarden.Core;
using TinyGarden.SaveSystem;

namespace TinyGarden.UI
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        
        [Header("Toggles")]
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle sfxToggle;
        [SerializeField] private Toggle voiceToggle;
        [SerializeField] private Toggle sensorySafeToggle;

        private bool isInitialized = false;

        private void Start()
        {
            if (settingsPanel != null)
                settingsPanel.SetActive(false);
        }

        public void OpenSettings()
        {
            if (settingsPanel != null)
            {
                settingsPanel.SetActive(true);
                LoadSettingsToUI();
            }
        }

        public void CloseSettings()
        {
            if (settingsPanel != null)
            {
                settingsPanel.SetActive(false);
            }
        }

        private void LoadSettingsToUI()
        {
            if (GameManager.Instance == null || GameManager.Instance.SaveSystem == null) return;
            var data = GameManager.Instance.SaveSystem.CurrentData;
            if (data == null || data.Settings == null) return;

            isInitialized = false; // Prevent events from firing during setup

            if (musicToggle != null) musicToggle.isOn = data.Settings.MusicEnabled;
            if (sfxToggle != null) sfxToggle.isOn = data.Settings.SfxEnabled;
            if (voiceToggle != null) voiceToggle.isOn = data.Settings.VoiceEnabled;
            if (sensorySafeToggle != null) sensorySafeToggle.isOn = data.Settings.SensorySafeModeEnabled;

            isInitialized = true;
        }

        public void OnMusicToggled(bool value)
        {
            if (!isInitialized) return;
            UpdateSetting((settings) => settings.MusicEnabled = value);
            if (!value) TinyGarden.Audio.AudioManager.Instance?.StopMusic();
            else TinyGarden.Audio.AudioManager.Instance?.PlayMusic("bgm_garden");
        }

        public void OnSfxToggled(bool value)
        {
            if (!isInitialized) return;
            UpdateSetting((settings) => settings.SfxEnabled = value);
        }

        public void OnVoiceToggled(bool value)
        {
            if (!isInitialized) return;
            UpdateSetting((settings) => settings.VoiceEnabled = value);
        }

        public void OnSensorySafeToggled(bool value)
        {
            if (!isInitialized) return;
            UpdateSetting((settings) => settings.SensorySafeModeEnabled = value);
        }

        private void UpdateSetting(System.Action<SettingsData> updateAction)
        {
            if (GameManager.Instance == null || GameManager.Instance.SaveSystem == null) return;
            var data = GameManager.Instance.SaveSystem.CurrentData;
            if (data == null) return;

            updateAction(data.Settings);
            GameManager.Instance.SaveSystem.Save(data);
        }

        public void OnResetProgressClicked()
        {
            if (GameManager.Instance != null && GameManager.Instance.SaveSystem != null)
            {
                GameManager.Instance.SaveSystem.ResetProgress();
                Debug.Log("[Settings] Progress has been reset.");
            }
        }
    }
}
