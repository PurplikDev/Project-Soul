using System.Linq;
using roguelike.core.item;
using roguelike.rendering.ui.slot;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class InventoryRenderer : ContainerRenderer {
        protected VisualElement _equipmentRoot, _trinketRoot;

        public InventoryRenderer(Inventory entityInventory, UIDocument inventoryUI) : base(entityInventory, inventoryUI) {
            root = inventoryUI.rootVisualElement;
            
            _equipmentRoot = root.Q<VisualElement>("EquipmentSlotContainer");
            _trinketRoot = root.Q<VisualElement>("TrinketSlotContainer");

            TranslateHeader(root.Q<Label>("InventoryHeader"));
            TranslateHeader(root.Q<Label>("CharacterHeader"));
            TranslateHeader(root.Q<Label>("EquipmentHeader"));
            TranslateHeader(root.Q<Label>("TrinketsHeader"));

            RegisterEquipmentSlots();
            RegisterTrinketSlots();
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            inventory.UpdateItemStack(clickedSlot.SlotStack, clickedSlot.SlotIndex);
        }



        // SLOT REGISTRATION METHODS

        private void RegisterEquipmentSlots() {
            foreach(EquipmentSlot equipmentSlot in _equipmentRoot.Children().ToList()) {
                equipmentSlot.SlotIndex = (int)equipmentSlot.SlotEquipmentType;
                equipmentSlot.ForceSetStack(inventory.Items[equipmentSlot.SlotIndex]);
                equipmentSlot.Renderer = this;
                itemSlots.Add(equipmentSlot);
                equipmentSlot.UpdateSlotEvent.Invoke();
            }
        }

        private void RegisterTrinketSlots() {
            int index = 0;
            foreach (EquipmentSlot equipmentSlot in _trinketRoot.Children().ToList()) {
                equipmentSlot.SlotIndex = (int)equipmentSlot.SlotEquipmentType + index;
                equipmentSlot.SetStack(inventory.Items[equipmentSlot.SlotIndex]);
                equipmentSlot.Renderer = this;
                itemSlots.Add(equipmentSlot);
                equipmentSlot.UpdateSlotEvent.Invoke();
                index++; // yes i know a for loop this exact thing, but using that would make this a bit more stupider
            }
        }
    }
}