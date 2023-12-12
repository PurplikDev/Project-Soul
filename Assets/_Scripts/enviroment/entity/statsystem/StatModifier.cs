using UnityEngine;
using static roguelike.enviroment.entity.statsystem.Stat;

namespace roguelike.enviroment.entity.statsystem {
    [SerializeField]
    public class StatModifier {
        public float ModifierValue { get; private set; }
        public StatModifierType ModifierType { get; private set; }
        public StatType StatType { get; private set; }

        public StatModifier(float value, StatModifierType modifierType, StatType statType) {
            ModifierValue = value;
            ModifierType = modifierType;
            StatType = statType;
        }

        public enum StatModifierType {
            FLAT = 0,
            ADDITIONAL = 1
        }
    }
}