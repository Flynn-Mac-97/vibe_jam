// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): the SfxKey / BgmKey dictionaries, the AudioSource pool, mixer group routing.
// Extend (GAMEPLAY): add Play3D(SfxKey, Vector3), pitch variation, or stingers below the GAMEPLAY marker.
// All gameplay audio MUST route through Play(SfxKey) / PlayBgm(BgmKey). Never place raw AudioSources on gameplay prefabs.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace VibeJam.Audio
{
    [System.Serializable]
    public class SfxEntry
    {
        public SfxKey key;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [System.Serializable]
    public class BgmEntry
    {
        public BgmKey key;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 0.8f;
    }

    public class AudioService : MonoBehaviour
    {
        // ---------- INFRA ----------
        public static AudioService Instance { get; private set; }

        [SerializeField] private AudioMixerGroup musicGroup;
        [SerializeField] private AudioMixerGroup sfxUiGroup;
        [SerializeField] private AudioMixerGroup sfxGameplayGroup;
        [SerializeField] private SfxEntry[] sfx;
        [SerializeField] private BgmEntry[] bgm;
        [SerializeField] private int sfxPoolSize = 6;

        private AudioSource musicSource;
        private readonly List<AudioSource> sfxPool = new List<AudioSource>();
        private readonly Dictionary<SfxKey, SfxEntry> sfxMap = new Dictionary<SfxKey, SfxEntry>();
        private readonly Dictionary<BgmKey, BgmEntry> bgmMap = new Dictionary<BgmKey, BgmEntry>();
        private bool initialized;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            Init();
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void Init()
        {
            if (initialized) return;
            initialized = true;

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
            if (musicGroup != null) musicSource.outputAudioMixerGroup = musicGroup;

            for (int i = 0; i < sfxPoolSize; i++)
            {
                var s = gameObject.AddComponent<AudioSource>();
                s.playOnAwake = false;
                sfxPool.Add(s);
            }

            if (sfx != null)
            {
                foreach (var e in sfx)
                {
                    if (e != null && e.key != SfxKey.None && !sfxMap.ContainsKey(e.key))
                        sfxMap.Add(e.key, e);
                }
            }
            if (bgm != null)
            {
                foreach (var e in bgm)
                {
                    if (e != null && e.key != BgmKey.None && !bgmMap.ContainsKey(e.key))
                        bgmMap.Add(e.key, e);
                }
            }
        }

        public void Play(SfxKey key)
        {
            if (!sfxMap.TryGetValue(key, out var entry) || entry.clip == null) return;
            var src = GetFreeSource();
            var isUi = key == SfxKey.UIClick || key == SfxKey.UIBack;
            src.outputAudioMixerGroup = isUi ? sfxUiGroup : sfxGameplayGroup;
            src.PlayOneShot(entry.clip, entry.volume);
        }

        public void PlayBgm(BgmKey key)
        {
            if (!bgmMap.TryGetValue(key, out var entry) || entry.clip == null) return;
            if (musicSource.clip == entry.clip && musicSource.isPlaying) return;
            musicSource.clip = entry.clip;
            musicSource.volume = entry.volume;
            musicSource.Play();
        }

        public void StopBgm()
        {
            if (musicSource != null) musicSource.Stop();
        }

        private AudioSource GetFreeSource()
        {
            foreach (var s in sfxPool)
                if (!s.isPlaying) return s;
            return sfxPool[0];
        }

        // ---------- GAMEPLAY ----------
        // Add pitch variation, positional playback, stingers here.
    }
}
