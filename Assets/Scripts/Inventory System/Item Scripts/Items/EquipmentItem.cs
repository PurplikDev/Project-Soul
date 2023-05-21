using io.purplik.ProjectSoul.Entity;
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
        [Space]
        [SerializeField] MeshRenderer mesh;

        public override Item GetCopy()
        {
            return Instantiate(this);
        }

        public override void Destory()
        {
            Destroy(this);
        }

        public virtual void Equip(LivingEntity livingEntity)
        {
            if (templarStat != 0)
                livingEntity.templar.AddModifier(new StatModifier(templarStat, StatType.FLAT, this));
            if (rogueStat != 0)
                livingEntity.rogue.AddModifier(new StatModifier(rogueStat, StatType.FLAT, this));
            if (thaumaturgeStat != 0)
                livingEntity.thaumaturge.AddModifier(new StatModifier(thaumaturgeStat, StatType.FLAT, this));

            if (healthBonus != 0)
                livingEntity.health.AddModifier(new StatModifier(healthBonus, StatType.FLAT, this));
            if (defenceBonus != 0)
                livingEntity.defence.AddModifier(new StatModifier(defenceBonus, StatType.FLAT, this));
            if (speedBonus != 0)
                livingEntity.speed.AddModifier(new StatModifier(speedBonus, StatType.FLAT, this));

            if (healthPercentBonus != 0)
                livingEntity.health.AddModifier(new StatModifier(healthPercentBonus, StatType.PERCENTAGEMULT, this));
            if (defencePercentBonus != 0)
                livingEntity.defence.AddModifier(new StatModifier(defencePercentBonus, StatType.PERCENTAGEMULT, this));
            if (speedPercentBonus != 0)
                livingEntity.speed.AddModifier(new StatModifier(speedPercentBonus, StatType.PERCENTAGEMULT, this));
        }

        public virtual void Unequip(LivingEntity livingEntity)
        {
            livingEntity.templar.RemoveAllModifiersFromSource(this);
            livingEntity.rogue.RemoveAllModifiersFromSource(this);
            livingEntity.thaumaturge.RemoveAllModifiersFromSource(this);
            
            livingEntity.health.RemoveAllModifiersFromSource(this);
            livingEntity.defence.RemoveAllModifiersFromSource(this);
            livingEntity.speed.RemoveAllModifiersFromSource(this);
        }
    }
}