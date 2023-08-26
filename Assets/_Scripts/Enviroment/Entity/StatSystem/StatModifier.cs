using UnityEngine;

namespace Roguelike.Enviroment.Entity.StatSystem {
    [SerializeField]
    public class StatModifier {
        public StatModifierType modifierType;
        public Stat.StatType statType;

        private float _modifierValue;

        public float ModifierValue { get { return _modifierValue; } }

        public enum StatModifierType {
            FLAT = 0,
            ADDITIONAL = 1
        }
    }
}