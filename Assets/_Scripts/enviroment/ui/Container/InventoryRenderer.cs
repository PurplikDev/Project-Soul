using System;
using System.Linq;
using roguelike.core.item;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class InventoryRenderer : ContainerRenderer {
        protected VisualElement _equipmentRoot, _trinketRoot;

        public InventoryRenderer(Inventory entityInventory, UIDocument inventoryUI) : base(entityInventory, inventoryUI) {
            _root = inventoryUI.rootVisualElement;
            
            _equipmentRoot = _root.Q<VisualElement>("EquipmentSlotContainer");
            _trinketRoot = _root.Q<VisualElement>("TrinketSlotContainer");

            RegisterEquipmentSlots();
            RegisterTrinketSlots();
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            _inventory.Items[clickedSlot.SlotIndex] = clickedSlot.SlotStack;
        }

        // SLOT REGISTRATION METHODS

        private void RegisterEquipmentSlots() {
            foreach(EquipmentSlot equipmentSlot in _equipmentRoot.Children().ToList()) {
                equipmentSlot.SlotIndex = (int)equipmentSlot.SlotEquipmentType;
                equipmentSlot.SetStack(_inventory.Items[equipmentSlot.SlotIndex]);
                equipmentSlot.Renderer = this;
                itemSlots.Add(equipmentSlot);
                equipmentSlot.UpdateSlotEvent.Invoke();
            }
        }

        private void RegisterTrinketSlots() {
            int index = 0;
            foreach (EquipmentSlot equipmentSlot in _trinketRoot.Children().ToList()) {
                equipmentSlot.SlotIndex = (int)equipmentSlot.SlotEquipmentType + index;
                equipmentSlot.SetStack(_inventory.Items[equipmentSlot.SlotIndex]);
                equipmentSlot.Renderer = this;
                itemSlots.Add(equipmentSlot);
                equipmentSlot.UpdateSlotEvent.Invoke();
                index++; // yes i know a for loop this exact thing, but using that would make this a bit more stupider
            }
        }
    }
}