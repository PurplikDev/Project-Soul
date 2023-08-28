using System.Collections.Generic;
using UnityEngine;

namespace roguelike.enviroment.entity.StatSystem {
    [SerializeField]
    public class Stat {
        private float _baseValue;

        protected List<StatModifier> statModifiers;

        public float Value { get { return ApplyModifiers(); } }
        public float BaseValue { get { return _baseValue; } } // no idea why i would need this, but still, just in case

        public float ApplyModifiers() {

            float returnValue = _baseValue;

            statModifiers.Sort(SortModifiers);

            foreach (StatModifier modifier in statModifiers) {
                switch (modifier.modifierType) {

                    case StatModifier.StatModifierType.FLAT: 
                        returnValue += modifier.ModifierValue;
                        break;

                    case StatModifier.StatModifierType.ADDITIONAL:
                        returnValue *= 1 + modifier.ModifierValue;
                        break;
                }
            }

            return returnValue;
        }

        private int SortModifiers(StatModifier firstModifier, StatModifier secondModifier) {
            if ((int) firstModifier.statType < (int) secondModifier.statType) {
                return -1;
            } else if ((int)firstModifier.statType > (int)secondModifier.statType) {
                return 1;
            }
            return 0;
        }

        public enum StatType {
            HEALTH,
            SPEED
        }
    }
}
