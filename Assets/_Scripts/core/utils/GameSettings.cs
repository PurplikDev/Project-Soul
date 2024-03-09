using System;

namespace roguelike.core.utils {
    [Serializable]
    public class GameSettings {

        public static event Action<GameSettings> GameSettingsChanged;

        public HealthBarStyle HealthBarStyle { get; set; } = HealthBarStyle.CLASSIC;
        public bool HealthBarText { get; set; } = true;
    }

    public enum HealthBarStyle {
        CLASSIC,
        BAR
    }
}