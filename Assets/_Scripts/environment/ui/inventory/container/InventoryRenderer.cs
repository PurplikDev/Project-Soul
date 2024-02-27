using System.Linq;
using roguelike.core.item;
using roguelike.rendering.ui.slot;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class InventoryRenderer : ContainerRenderer {
        protected VisualElement equipmentRoot, trinketRoot, itemTooltip;

        protected Label tooltipName, tooltipDescription;

        public InventoryRenderer(Inventory entityInventory, UIDocument inventoryUI) : base(entityInventory, inventoryUI) {
            root = inventoryUI.rootVisualElement;
            
            equipmentRoot = root.Q<VisualElement>("EquipmentSlotContainer");
            trinketRoot = root.Q<VisualElement>("TrinketSlotContainer");

            itemTooltip = root.Q<VisualElement>("ItemTooltip");

            tooltipName = itemTooltip.Q<Label>("ItemTooltipName");
            tooltipDescription = itemTooltip.Q<Label>("ItemTooltipDescription");

            TranslateHeader(root.Q<Label>("InventoryHeader"));
            TranslateHeader(root.Q<Label>("CharacterHeader"));
            TranslateHeader(root.Q<Label>("EquipmentHeader"));
            TranslateHeader(root.Q<Label>("TrinketsHeader"));

            RegisterEquipmentSlots();
            RegisterTrinketSlots();

            foreach(ItemSlot slot in itemSlots) {
                slot.RegisterCallback<PointerOverEvent>(ShowTooltip);
                slot.RegisterCallback<PointerOutEvent>(HideTooltip);
            }
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            inventory.UpdateItemStack(clickedSlot.SlotStack, clickedSlot.SlotIndex);
        }



        // SLOT REGISTRATION METHODS

        private void RegisterEquipmentSlots() {
            foreach(EquipmentSlot equipmentSlot in equipmentRoot.Children().ToList()) {
                equipmentSlot.SlotIndex = (int)equipmentSlot.SlotEquipmentType;
                equipmentSlot.ForceSetStack(inventory.Items[equipmentSlot.SlotIndex]);
                equipmentSlot.Renderer = this;
                itemSlots.Add(equipmentSlot);
                equipmentSlot.UpdateSlotEvent.Invoke();
            }
        }

        private void RegisterTrinketSlots() {
            int index = 0;
            foreach (EquipmentSlot equipmentSlot in trinketRoot.Children().ToList()) {
                equipmentSlot.SlotIndex = (int)equipmentSlot.SlotEquipmentType + index;
                equipmentSlot.SetStack(inventory.Items[equipmentSlot.SlotIndex]);
                equipmentSlot.Renderer = this;
                itemSlots.Add(equipmentSlot);
                equipmentSlot.UpdateSlotEvent.Invoke();
                index++; // yes i know a for loop this exact thing, but using that would make this a bit more stupider
            }
        }



        private void ShowTooltip(PointerOverEvent evt) {
            if(evt.currentTarget is ItemSlot slot && !slot.SlotStack.IsEmpty()) {
                tooltipName.text = slot.SlotStack.Item.Name;
                tooltipDescription.text = slot.SlotStack.Item.Description;

                itemTooltip.style.visibility = Visibility.Visible;
            }
        }

        private void HideTooltip(PointerOutEvent evt) {
            itemTooltip.style.visibility = Visibility.Hidden;
        }
    }
}