using roguelike.environment.entity.statsystem;

namespace roguelike.core.item {
    public class Dagger : WeaponItem {
        public Dagger(string id, int value, float damage, float swingSpeed, float attackCooldown, int weaponTier, params StatModifier[] modifiers) 
            : base(id, value, damage, swingSpeed, attackCooldown, EquipmentType.MAIN_HAND, false, weaponTier, modifiers) {
        }
    }
}