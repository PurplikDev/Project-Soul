using roguelike.environment.entity.statsystem;

namespace roguelike.core.item {
    public class WeaponItem : HandheldItem {

        public int WeaponTier { get; private set; }
        public float Damage { get; private set; }
        public float SwingSpeed { get; private set; }
        public float AttackCooldown { get; private set; }

        public WeaponItem(string id, int value, float damage, float swingSpeed, float attackCooldown, EquipmentType type, bool isTwoHanded, int weaponTier, params StatModifier[] modifiers)
            : base(id, value, type, isTwoHanded, modifiers) {
            Damage = damage;
            WeaponTier = weaponTier;
            SwingSpeed = swingSpeed;
            AttackCooldown = attackCooldown;
        }
    }
}
