using roguelike.enviroment.entity;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class LightSword : WeaponItem {
        public LightSword(string id, float damage, int weaponTier, params StatModifier[] modifiers) 
            : base(id, damage, EquipmentType.MAIN_HAND, false, weaponTier, modifiers) {}

        public override void WeaponAction(Entity entityAttacker) {
            Debug.Log(entityAttacker.name + " is attacking with " + Name + " for " + Damage + " damage!");
        }
    }
}

