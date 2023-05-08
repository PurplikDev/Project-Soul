using io.purplik.ProjectSoul.Entity.Stats;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public enum EquipmentType
    {
        HELMET,
        CHESTPLATE,
        LEGGINGS,
        BOOTS,
        WEAPON_MAIN
    }

    [CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory System/Equipment Item")]
    public class EquipmentItem : Item
    {
        public int rogueStat;
        public int thaumaturgeStat; 
        public int templarStat;
        [Space]
        public int healthBonus;
        public int defenceBonus;
        public int speedBonus;
        [Space]
        public float healthPercentBonus;
        public float defencePercentBonus;
        public float speedPercentBonus;
        [Space]
        public EquipmentType equipmentType;

        public void Equip(Character character)
        {
            if (templarStat != 0)
                character.templar.AddModifier(new StatModifier(templarStat, StatType.FLAT, this));
            if (rogueStat != 0)
                character.rogue.AddModifier(new StatModifier(rogueStat, StatType.FLAT, this));
            if (thaumaturgeStat != 0)
                character.thaumaturge.AddModifier(new StatModifier(thaumaturgeStat, StatType.FLAT, this));

            if (healthBonus != 0)
                character.health.AddModifier(new StatModifier(healthBonus, StatType.FLAT, this));
            if (defenceBonus != 0)
                character.defence.AddModifier(new StatModifier(defenceBonus, StatType.FLAT, this));
            if (speedBonus != 0)
                character.speed.AddModifier(new StatModifier(speedBonus, StatType.FLAT, this));

            if (healthPercentBonus != 0)
                character.health.AddModifier(new StatModifier(healthPercentBonus, StatType.PERCENTAGEMULT, this));
            if (defencePercentBonus != 0)
                character.defence.AddModifier(new StatModifier(defencePercentBonus, StatType.PERCENTAGEMULT, this));
            if (speedPercentBonus != 0)
                character.speed.AddModifier(new StatModifier(speedPercentBonus, StatType.PERCENTAGEMULT, this));
        }

        public void Unequip(Character character)
        {
            character.templar.RemoveAllModifiersFromSource(this);
            character.rogue.RemoveAllModifiersFromSource(this);
            character.thaumaturge.RemoveAllModifiersFromSource(this);
            
            character.health.RemoveAllModifiersFromSource(this);
            character.defence.RemoveAllModifiersFromSource(this);
            character.speed.RemoveAllModifiersFromSource(this);
        }
    }
}