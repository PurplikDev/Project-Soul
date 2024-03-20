using Newtonsoft.Json;
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

        static GameSettings() {
            GameSettingsChanged += SaveSettings;
        }

        public GameSettings(float masterVolume, float musicVolume, float sfxVolume, HealthBarStyle healthStyle, bool healthText, Language lang) {
            MasterVolume = masterVolume;
            MusicVolume = musicVolume;
            SFXVolume = sfxVolume;
            HealthBarStyle = healthStyle;
            HealthBarText = healthText;
            Lang = lang;
        }

        internal static GameSettings GetOrCreateSettings() {
            if(File.Exists(GlobalStaticValues.SETTINGS_PATH)) {
                return JsonConvert.DeserializeObject<GameSettings>(File.ReadAllText(GlobalStaticValues.SETTINGS_PATH));
            } else {
                return new GameSettings(0f, 0f, 0f, HealthBarStyle.CLASSIC, true, Language.en_us);
            }
        }

        internal static void SaveSettings(GameSettings settings) {
            string output = JsonConvert.SerializeObject(settings);

            Directory.CreateDirectory(Path.GetDirectoryName(GlobalStaticValues.SETTINGS_PATH));

            using (FileStream fileStream = new FileStream(GlobalStaticValues.SETTINGS_PATH, FileMode.Create)) {
                byte[] info = new UTF8Encoding(true).GetBytes(output);
                fileStream.Write(info, 0, info.Length);
            }
        }
    }

    public enum HealthBarStyle {
        CLASSIC,
        BAR
    }
}