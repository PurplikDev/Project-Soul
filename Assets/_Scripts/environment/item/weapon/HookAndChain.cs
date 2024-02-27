using roguelike.environment.entity.statsystem;

namespace roguelike.core.item {
    public class HookAndChain : WeaponItem {
        public HookAndChain()
            : base("hook_and_chain", 256, 5f, 1.5f, 0.5f, EquipmentType.OFF_HAND, false, 2) {
        }
    }
}
