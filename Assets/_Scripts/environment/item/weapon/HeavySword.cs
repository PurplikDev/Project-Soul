using roguelike.environment.entity.statsystem;

namespace roguelike.core.item {
    public class HeavySword : WeaponItem {
        public HeavySword(string id, float damage, float swingSpeed, float attackCooldown, int weaponTier, params StatModifier[] modifiers)
            : base(id, damage, swingSpeed, attackCooldown, EquipmentType.MAIN_HAND, true, weaponTier, modifiers) {
        }
    }
}