using roguelike.environment.entity;
using roguelike.environment.entity.combat;
using roguelike.environment.entity.statsystem;
using UnityEngine;

namespace roguelike.core.item {
    public class LightSword : WeaponItem {
        public LightSword(string id, int value, float damage, float swingSpeed, float attackCooldown, int weaponTier, params StatModifier[] modifiers) 
            : base(id, value, damage, swingSpeed, attackCooldown, EquipmentType.MAIN_HAND, false, weaponTier, modifiers) {}

        public override void ItemAction(Entity entityAttacker) {
            if(Physics.Raycast(entityAttacker.Position, entityAttacker.LookDirection, out var hitInfo, 5, LayerMask.GetMask("Entity"))) {
                var entity = hitInfo.transform.GetComponent<Entity>();
                entity?.Damage(new DamageSource(Damage, DamageType.COMBAT, WeaponTier, entity, entityAttacker));
            }
        }
    }
}

