using System;
using System.Collections.Generic;

namespace roguelike.enviroment.entity.statsystem {
    [Serializable]
    public class Stat {
        private float _baseValue;

        private float cachedValue;
        private bool _isDirty = true;

        protected List<StatModifier> statModifiers;

        public float Value {
            get {
                 cachedValue = _isDirty ? ApplyModifiers() : cachedValue;
                 _isDirty = false;
                 return cachedValue;
            }
        }
        public float BaseValue { get { return _baseValue; } }
        // no idea why i would need this, but still, just in case

        public Stat(int baseValue) {
            _baseValue = baseValue;
            statModifiers = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier) {
            _isDirty = true;
            statModifiers.Add(modifier);
            statModifiers.Sort(SortModifiers);
        }

        public void RemoveModifier(StatModifier modifier) {
            _isDirty = true;
            statModifiers.Remove(modifier);
        }

        protected virtual float ApplyModifiers() {
            float returnValue = _baseValue;

            for(int i = 0; i < statModifiers.Count; i++) {
                StatModifier modifier = statModifiers[i];

                switch (modifier.ModifierType) {

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
            if ((int) firstModifier.ModifierType < (int) secondModifier.ModifierType) {
                return -1;
            } else if ((int)firstModifier.ModifierType > (int)secondModifier.ModifierType) {
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
