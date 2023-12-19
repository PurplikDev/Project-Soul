using roguelike.enviroment.entity;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class Shield : HandheldItem {

        public bool IsUp { get; private set; }

        StatModifier slowDown;

        public Shield(string id, float slowdownEffect, int weaponTier, params StatModifier[] modifiers)
            : base(id, EquipmentType.OFF_HAND, false, modifiers) {
            IsUp = false;
            slowDown = new StatModifier(slowdownEffect, StatModifier.StatModifierType.ADDITIONAL, Stat.StatType.SPEED);
        }

        public override void ItemAction(Entity entityAttacker) {
            IsUp = !IsUp;
            if(IsUp) {
                entityAttacker.Speed.AddModifier(slowDown);
                entityAttacker.IsBlocking = true;
                Debug.Log("shield is up!");
            } else {
                entityAttacker.Speed.RemoveModifier(slowDown);
                entityAttacker.IsBlocking = false;
                Debug.Log("shield is down!");
            }
        }
    }
}