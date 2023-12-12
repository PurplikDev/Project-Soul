using System.Collections.Generic;
using System.Linq;
using roguelike.enviroment.entity.statsystem;

namespace roguelike.core.item {
    public class EquipmentItem : Item {
        public List<StatModifier> StatModifiers;

        private EquipmentType _itemEquipmentType;

        public EquipmentType ItemEquipmentType { get { return _itemEquipmentType; } }

        public EquipmentItem(string id, EquipmentType type, params StatModifier[] modifiers) : base(id) {
            _itemEquipmentType = type;
            StatModifiers = modifiers.ToList();
        }
    }

    public enum EquipmentType {
        HELMET = 20,
        CHESTPLATE = 21,
        PANTS = 22,
        BOOTS = 23,
        MAIN_HAND = 24,
        OFF_HAND = 25,
        TRINKET = 26
    }
}