using System;
using System.Collections.Generic;
using UnityEngine;

namespace TinyGarden.Audio
{
    [Serializable]
    public class AudioEntry
    {
        public string id;
        public AudioClip clip;
    }

    [CreateAssetMenu(fileName = "AudioCatalog", menuName = "Tiny Garden/Audio/Audio Catalog")]
    public class AudioCatalog : ScriptableObject
    {
        [SerializeField] private List<AudioEntry> entries = new List<AudioEntry>();

        public AudioClip GetClip(string id)
        {
            var entry = entries.Find(e => e.id == id);
            return entry?.clip;
        }
    }
}
