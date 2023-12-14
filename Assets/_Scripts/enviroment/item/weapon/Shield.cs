using roguelike.enviroment.entity;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class Shield : WeaponItem { // todo: change this to be equipment, not weapon

        public bool IsUp { get; private set; }

        StatModifier slowDown;

        public Shield(string id, float slowdownEffect, int weaponTier, params StatModifier[] modifiers)
            : base(id, 0f, EquipmentType.OFF_HAND, false, weaponTier, modifiers) {
            IsUp = false;
            slowDown = new StatModifier(slowdownEffect, StatModifier.StatModifierType.ADDITIONAL, Stat.StatType.SPEED);
        }

        public override void WeaponAction(Entity entityAttacker) {
            IsUp = !IsUp;
            if(IsUp) {
                entityAttacker.Speed.AddModifier(slowDown);
                Debug.Log("shield is up!");
            } else {
                entityAttacker.Speed.RemoveModifier(slowDown);
                Debug.Log("shield is down!");
            }
        }
    }
}