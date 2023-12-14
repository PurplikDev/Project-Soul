using roguelike.enviroment.entity.statsystem;

namespace roguelike.core.item {
    public class HeavySword : WeaponItem {
        public HeavySword(string id, float damage, int weaponTier, params StatModifier[] modifiers)
            : base(id, damage, EquipmentType.MAIN_HAND, true, weaponTier, modifiers) {
        }
    }
}
