using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace io.purplik.ProjectSoul.Entity.Stats
{
    [Serializable]
    public class EntityStat
    {
        public float baseValue;

        public virtual float Value { 
            get { 
                if(isDirty || baseValue != lastBaseValue)
                {
                    lastBaseValue = baseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }

                return _value;
            } 
        }

        protected bool isDirty = true;
        protected float _value;
        protected float lastBaseValue = float.MinValue;

        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> _statModifiers;

        public EntityStat()
        {
            statModifiers = new List<StatModifier>();
            _statModifiers = statModifiers.AsReadOnly();
        }

        public EntityStat(float baseValue) : this()
        {
            this.baseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier modifier)
        {
            isDirty = true;
            statModifiers.Add(modifier);
            statModifiers.Sort(CompareModifierOrder);
        }

        protected virtual int CompareModifierOrder(StatModifier firstModifier, StatModifier secondModifier)
        {
            if(firstModifier.order < secondModifier.order)
            {
                return -1;
            } else if(firstModifier.order > secondModifier.order)
            {
                return 1;
            } 
            
            return 0;
        }

        public virtual bool RemoveModifier(StatModifier modifier)
        {
            if (statModifiers.Remove(modifier))
            {
                isDirty = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;
            for(int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if(statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPrecentAdd = 0;

            for(int i= 0; i < statModifiers.Count; i++)
            {
                StatModifier modifier = statModifiers[i];

                if(modifier.type == StatType.FLAT)
                {
                    finalValue += statModifiers[i].value;
                } else if(modifier.type == StatType.PERCENTAGEMULT)
                {
                    finalValue *= 1 + modifier.value;
                } else if (modifier.type == StatType.PERCENTAGEADD)
                {
                    sumPrecentAdd += modifier.value;

                    if(i + i >= statModifiers.Count || statModifiers[i + 1].type != StatType.PERCENTAGEADD)
                    {
                        finalValue *= 1 + sumPrecentAdd;
                        sumPrecentAdd = 0;
                    }
                } else if (modifier.type == StatType.OVERRIDE)
                {
                    finalValue = statModifiers[i].value;
                }

            }

            return (float)Math.Round(finalValue, 1);
        }
    }
}
