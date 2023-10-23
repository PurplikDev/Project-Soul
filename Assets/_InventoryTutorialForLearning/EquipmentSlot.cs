using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using roguelike.enviroment.item.equipment;
using System;

namespace roguelike.enviroment.item.renderer {
    public class EquipmentSlot : ItemSlot {

        public EquipmentType slotEquipment { get; set; }

        public EquipmentSlot(EquipmentType type) : base() {
            slotEquipment = type;
        }

        /*
        public new class UxmlFactory : UxmlFactory<EquipmentSlot, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits {
            UxmlEnumAttributeDescription<EquipmentType> slotEnum = new UxmlEnumAttributeDescription<EquipmentType> {
                name = "equipment-type",
                obsoleteNames = new string[1] { "equipmentType" }
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
                base.Init(ve, bag, cc);
                var ate = ve as EquipmentSlot;

                ate.slotEquipment = slotEnum.GetValueFromBag(bag, cc);
            }
        }
        */
    }
}