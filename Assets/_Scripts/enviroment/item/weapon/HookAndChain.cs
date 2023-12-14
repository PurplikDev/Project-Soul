using roguelike.enviroment.entity.statsystem;

namespace roguelike.core.item {
    public class HookAndChain : WeaponItem {
        public HookAndChain(string id, int weaponTier, params StatModifier[] modifiers)
            : base(id, 5f, EquipmentType.OFF_HAND, false, weaponTier, modifiers) {
        }
    }
}
