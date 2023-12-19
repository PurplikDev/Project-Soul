using roguelike.enviroment.entity;
using roguelike.enviroment.entity.combat;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class LightSword : WeaponItem {
        public LightSword(string id, float damage, int weaponTier, params StatModifier[] modifiers) 
            : base(id, damage, EquipmentType.MAIN_HAND, false, weaponTier, modifiers) {}

        public override void ItemAction(Entity entityAttacker) {
            if(Physics.Raycast(entityAttacker.Position, entityAttacker.LookDirection, out var hitInfo, 5, LayerMask.GetMask("Entity"))) {
                var entity = hitInfo.transform.GetComponent<Entity>();
                entity?.Damage(new DamageSource(Damage, DamageType.COMBAT, WeaponTier, entity, entityAttacker));
            }
        }
    }
}

