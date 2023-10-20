using System.Collections.Generic;
using roguelike.enviroment.entity.player.inventory;
using roguelike.enviroment.item.container;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.item.renderer {
    public class InventoryController {
        private VisualElement _root;
        private VisualElement _inventorySlotContainer, _equipmentSlotContainer, _trinketSlotContainer;

        public List<ItemSlot> InventoryItemSlots, EquipmentItemSlots, TrinketItemSlot = new List<ItemSlot>();

        public Dictionary<ItemSlot, Slot> slots = new Dictionary<ItemSlot, Slot>();

        public ItemSlot MouseSlot;

        public InventoryController(UIDocument document, Inventory inventory) {
            _root = document.rootVisualElement;
            MouseSlot = _root.Q<ItemSlot>("MouseSlot");

            _inventorySlotContainer = _root.Q<VisualElement>("InventorySlotContainer");
            _equipmentSlotContainer = _root.Q<VisualElement>("EquipmentSlotContainer");
            _trinketSlotContainer = _root.Q<VisualElement>("TrinketSlotContainer");
            /*
            _registerSlots(inventory.InventoryItems, _inventorySlotContainer, InventoryItemSlots);
            _registerSlots(inventory.EquipmentItems, _equipmentSlotContainer, EquipmentItemSlots);
            _registerSlots(inventory.TrinketItems, _trinketSlotContainer, TrinketItemSlot);

            Debug.Log(slots.Count);
            Debug.Log(_inventorySlotContainer.childCount);
            Debug.Log(_equipmentSlotContainer.childCount);
            Debug.Log(_trinketSlotContainer.childCount);
            */
        }

        private void _registerSlots(List<Slot> inventorySlotList, VisualElement root, List<ItemSlot> itemSlots) {
            foreach(Slot slot in inventorySlotList) {
                ItemSlot itemSlot = new ItemSlot();

                root.Add(itemSlot);
                itemSlots.Add(itemSlot);

                slots.Add(itemSlot, slot);
            }
        }
    }
}