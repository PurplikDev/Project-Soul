using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Enviroment.Entity.StatSystem {
    [SerializeField]
    public class Stat {
        private float _value;
        private float _baseValue;

        protected StatType type;
        protected List<StatModifier> statModifiers;

        public float Value { get { return ApplyModifiers(); } }
        public float BaseValue { get { return _baseValue; } }

        public float ApplyModifiers() {

            float returnValue = BaseValue; // YES I KNOW I CAN JUST REFERENCE THE _baseValue VARIABLE BUT I JUST WANT TO DO IT LIKE THIS

            statModifiers.Sort(SortModifiers);

            foreach (StatModifier modifier in statModifiers) {
                    if (modifier.statType == type) {
                    switch (modifier.modifierType) {

                        case StatModifier.StatModifierType.FLAT: 
                            returnValue += modifier.ModifierValue;
                            break;

                        case StatModifier.StatModifierType.ADDITIONAL:
                            returnValue *= 1 + modifier.ModifierValue;
                            break;
                    }
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
