using System;
using System.Collections.Generic;
using UnityEngine;

namespace roguelike.enviroment.entity.StatSystem {
    [Serializable]
    public class Stat {
        [SerializeField] private float _baseValue;
        
        protected List<StatModifier> statModifiers;

        public float Value { get { return ApplyModifiers(); } }
        public float BaseValue { get { return _baseValue; } } // no idea why i would need this, but still, just in case

        public Stat(int baseValue) {
            _baseValue = baseValue;
        }

        public void AddModifier(StatModifier modifier) { 
            statModifiers.Add(modifier);
            statModifiers.Sort(SortModifiers);
        }

        public float ApplyModifiers() {

            float returnValue = _baseValue;

            for(int i = 0; i < statModifiers.Count; i++) {
                StatModifier modifier = statModifiers[i];

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
            if ((int) firstModifier.modifierType < (int) secondModifier.modifierType) {
                return -1;
            } else if ((int)firstModifier.modifierType > (int)secondModifier.modifierType) {
                return 1;
            }
            return 0;
        }

        public enum StatType {
            HEALTH,
            SPEED,
            DEFENCE,
            TEMPLAR,
            ROGUE,
            THAUMATURGE
        }
    }
}
