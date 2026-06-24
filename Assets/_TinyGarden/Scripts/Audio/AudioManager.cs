using UnityEngine;

namespace TinyGarden.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioCatalog catalog;
        
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource voiceSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeSources();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeSources()
        {
            if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();
            if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
            if (voiceSource == null) voiceSource = gameObject.AddComponent<AudioSource>();

            musicSource.loop = true;
            sfxSource.loop = false;
            voiceSource.loop = false;
        }

        public void PlaySfx(string cueId)
        {
            if (!IsSfxEnabled()) return;
            AudioClip clip = GetClip(cueId);
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        public void PlayVoice(string cueId)
        {
            if (!IsVoiceEnabled()) return;
            AudioClip clip = GetClip(cueId);
            if (clip != null)
            {
                // Optionally stop current voice before playing new one
                voiceSource.Stop();
                voiceSource.clip = clip;
                voiceSource.Play();
            }
        }

        public void PlayMusic(string cueId)
        {
            if (!IsMusicEnabled()) return;
            AudioClip clip = GetClip(cueId);
            if (clip != null)
            {
                if (musicSource.clip == clip && musicSource.isPlaying) return;
                musicSource.clip = clip;
                musicSource.Play();
            }
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        private bool IsMusicEnabled()
        {
            var sm = TinyGarden.Core.GameManager.Instance?.SaveSystem;
            if (sm != null && sm.CurrentData != null && sm.CurrentData.Settings != null)
            {
                return sm.CurrentData.Settings.MusicEnabled;
            }
            return true;
        }

        private bool IsSfxEnabled()
        {
            var sm = TinyGarden.Core.GameManager.Instance?.SaveSystem;
            if (sm != null && sm.CurrentData != null && sm.CurrentData.Settings != null)
            {
                return sm.CurrentData.Settings.SfxEnabled;
            }
            return true;
        }

        private bool IsVoiceEnabled()
        {
            var sm = TinyGarden.Core.GameManager.Instance?.SaveSystem;
            if (sm != null && sm.CurrentData != null && sm.CurrentData.Settings != null)
            {
                return sm.CurrentData.Settings.VoiceEnabled;
            }
            return true;
        }

        private AudioClip GetClip(string cueId)
        {
            if (catalog == null)
            {
                Debug.LogWarning($"[AudioManager] AudioCatalog is missing. Cannot play {cueId}");
                return null;
            }

            AudioClip clip = catalog.GetClip(cueId);
            if (clip == null)
            {
                Debug.LogWarning($"[AudioManager] Clip for cue '{cueId}' not found in catalog.");
            }
            return clip;
        }
    }
}
