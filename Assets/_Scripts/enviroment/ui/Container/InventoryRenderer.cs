using System;
using System.Linq;
using roguelike.core.item;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class InventoryRenderer : ContainerRenderer {
        protected VisualElement _inventoryRoot, _equipmentRoot, _trinketRoot;

        protected Inventory _inventory;

        public InventoryRenderer(Inventory entityInventory, UIDocument inventoryUI) : base(inventoryUI) {
            _root = inventoryUI.rootVisualElement;

            _inventoryRoot = _root.Q<VisualElement>("InventorySlotContainer");
            _equipmentRoot = _root.Q<VisualElement>("EquipmentSlotContainer");
            _trinketRoot = _root.Q<VisualElement>("TrinketSlotContainer");

            _inventory = entityInventory;

            CreateSlots();

            RegisterEquipmentSlots();
            RegisterTrinketSlots();
        }

        protected override void SyncInternalToVisual() {
            
        }

        protected override void SyncInternalToVisualSingle(ItemSlot clickedSlot) {
            
        }

        protected override void SyncVisualToInternal() {
            throw new NotImplementedException();
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            throw new NotImplementedException();
        }



        // SLOT CREATION AND REGISTRATION METHODS

        private void CreateSlots() {
            for (int i = 0; i < Inventory.InventorySize; i++) {
                ItemSlot itemSlot = new ItemSlot();
                itemSlot.SlotIndex = i;
                itemSlot.SetStack(_inventory.Items[i]);
                itemSlot.Renderer = this;
                itemSlots.Add(itemSlot);
                _inventoryRoot.Add(itemSlot);
                itemSlot.UpdateSlotEvent.Invoke();
            }
        }

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