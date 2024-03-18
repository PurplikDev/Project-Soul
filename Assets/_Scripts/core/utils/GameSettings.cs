using Newtonsoft.Json;
using roguelike.system.manager;
using System;
using System.IO;
using System.Text;
using static TranslationManager;

namespace roguelike.core.utils {
    [Serializable]
    public class GameSettings {
        public static Action<GameSettings> GameSettingsChanged;

        public float MasterVolume, MusicVolume, SFXVolume;
        public HealthBarStyle HealthBarStyle { get; set; }
        public bool HealthBarText { get; set; }
        public Language Lang;

        public GameSettings(float masterVolume, float musicVolume, float sfxVolume, HealthBarStyle healthStyle, bool healthText, Language lang) {
            MasterVolume = masterVolume;
            MusicVolume = musicVolume;
            SFXVolume = sfxVolume;
            HealthBarStyle = healthStyle;
            HealthBarText = healthText;
            Lang = lang;
        }
    }

    public enum HealthBarStyle {
        CLASSIC,
        BAR
    }
}