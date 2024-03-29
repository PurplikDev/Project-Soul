using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace roguelike.environment.entity.statsystem {
    [Serializable]
    public class Stat {
        private float cachedValue;
        private bool _isDirty = true;

        protected List<StatModifier> statModifiers;

        public float BaseValue;
        public float Value {
            get {
                 cachedValue = _isDirty ? ApplyModifiers() : cachedValue;
                 _isDirty = false;
                 return cachedValue;
            }
        }

        public Stat(float baseValue) {
            BaseValue = baseValue;
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
            float returnValue = BaseValue;

            for(int i = 0; i < statModifiers.Count; i++) {
                StatModifier modifier = statModifiers[i];

                switch (modifier.ModifierType) {

                    case StatModifierType.FLAT: 
                        returnValue += modifier.ModifierValue;
                        break;

                    case StatModifierType.ADDITIONAL:
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
