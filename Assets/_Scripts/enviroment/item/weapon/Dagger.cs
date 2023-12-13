using roguelike.enviroment.entity.statsystem;

namespace roguelike.core.item {
    public class Dagger : WeaponItem {
        public Dagger(string id, EquipmentType type, int weaponTier, params StatModifier[] modifiers) : base(id, type, weaponTier, modifiers) {
        }
    }
}