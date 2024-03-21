using roguelike.core.utils;
using roguelike.system.singleton;
using UnityEngine;
using UnityEngine.Audio;

namespace roguelike.system.manager {
    public class AudioManager : PersistentSingleton<AudioManager> {
        public AudioMixer Mixer;

        private void Start() {
            ApplySettings(GameManager.CurrentGameSettings);
            GameSettings.GameSettingsChanged += ApplySettings;
        }

        public void ApplySettings(GameSettings settings) {
            Mixer.SetFloat("Master", Mathf.Log10(settings.MasterVolume) * 20f);
            Mixer.SetFloat("Music", Mathf.Log10(settings.MusicVolume) * 20f);
            Mixer.SetFloat("SFX", Mathf.Log10(settings.SFXVolume) *20f);
        }
    }
}