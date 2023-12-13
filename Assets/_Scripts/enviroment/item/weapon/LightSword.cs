using roguelike.enviroment.entity.statsystem;

namespace roguelike.core.item {
    public class LightSword : WeaponItem {
        public LightSword(string id, EquipmentType type, int weaponTier, params StatModifier[] modifiers) 
            : base(id, type, weaponTier, modifiers) {}


    }
}

