using io.purplik.ProjectSoul.Entity.Stats;
using io.purplik.ProjectSoul.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    [CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory System/Weapon Item")]
    public class WeaponItem : EquipmentItem
    {
        public int damage;
        public Transform model;

        public override void Equip(LivingEntity livingEntity)
        {
            base.Equip(livingEntity);
            if (damage != 0)
                livingEntity.damage.AddModifier(new StatModifier(damage, StatType.FLAT, this));
        }

        public override void Unequip(LivingEntity livingEntity)
        {
            base.Unequip(livingEntity);
            livingEntity.damage.RemoveAllModifiersFromSource(this);
        }
    }
}