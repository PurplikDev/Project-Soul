using System;
using System.Xml.Serialization;
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
        [XmlEnum(Name = "Weapon")] WEAPON,
        [XmlEnum(Name = "Helmet")] HELMET,
        [XmlEnum(Name = "Chestplate")] CHESTPLATE,
        [XmlEnum(Name = "Legs")] LEGS,
        [XmlEnum(Name = "Boots")] BOOTS,
        [XmlEnum(Name = "Trinket")] TRINKET
    }
}