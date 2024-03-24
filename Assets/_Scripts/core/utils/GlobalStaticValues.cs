using roguelike.environment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.utils {
    public class GlobalStaticValues {
        public static readonly string EMPTY_STACK_WARNING = "You are trying to interact with an empty slot!";

        public static readonly string SHAPED_RECIPE_SIZE_ERROR = "Shaped recipe isn't the correct size!";

        public static readonly string SAVE_PATH = Application.persistentDataPath + "/GameSaves";
        public static readonly string SETTINGS_PATH = Application.persistentDataPath + "/settings.json";



        public static readonly StatModifier TEMPLAR_BONUS_STAT = new StatModifier(2, StatModifierType.FLAT, StatType.DEFENCE);
        public static readonly StatModifier ROGUE_BONUS_STAT = new StatModifier(2, StatModifierType.FLAT, StatType.SPEED);
        public static readonly StatModifier THAUMATURGE_BONUS_STAT = new StatModifier(20, StatModifierType.FLAT, StatType.HEALTH);
    }
}