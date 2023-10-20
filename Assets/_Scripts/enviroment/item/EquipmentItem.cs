using System;
using UnityEngine;

namespace roguelike.enviroment.item.equipment {
    [Serializable]
    public class EquipmentItem : Item {
        private EquipmentType _equipmentType;

        public EquipmentItem(string id, EquipmentType equipmentType) : base(id) {
            _equipmentType = equipmentType;
        }

        // getters and setters
        public EquipmentType EquipmentType { get { return _equipmentType; } }
    }

    public enum EquipmentType
    {
        WEAPON,
        HELMET,
        CHESTPLATE,
        LEGS,
        BOOTS,
        TRINKET
    }
}