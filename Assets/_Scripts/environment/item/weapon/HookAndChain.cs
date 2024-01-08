using roguelike.environment.entity.statsystem;

namespace roguelike.core.item {
    public class HookAndChain : WeaponItem {
        public HookAndChain(string id, int weaponTier, float swingSpeed, float attackCooldown, params StatModifier[] modifiers)
            : base(id, 5f, swingSpeed, attackCooldown, EquipmentType.OFF_HAND, false, weaponTier, modifiers) {
        }
    }
}
