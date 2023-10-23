using roguelike.enviroment.entity.player.inventory;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.item.renderer {
    public class InventoryController {
        private VisualElement _root;
        private VisualElement _inventorySlotContainer, _equipmentSlotContainer, _trinketSlotContainer;

        public List<ItemSlot> InventoryItemSlots, EquipmentItemSlots, TrinketItemSlot = new List<ItemSlot>();

        public Dictionary<ItemSlot, ItemStack> slots = new Dictionary<ItemSlot, ItemStack>();

        public ItemSlot MouseSlot;

        public InventoryController(Inventory inventory) {
            
            /*
            _root = document.rootVisualElement;
            MouseSlot = _root.Q<ItemSlot>("MouseSlot");

            _inventorySlotContainer = _root.Q<VisualElement>("InventorySlotContainer");
            _equipmentSlotContainer = _root.Q<VisualElement>("EquipmentSlotContainer");
            _trinketSlotContainer = _root.Q<VisualElement>("TrinketSlotContainer");

            foreach(ItemStack itemStack in inventory.InventoryItems) {
                ItemSlot itemSlot = new ItemSlot();

                slots.Add(itemSlot, itemStack);
                InventoryItemSlots.Add(itemSlot);
                _inventorySlotContainer.Add(itemSlot);
            }*/
        }

        public static void DragItem(ItemSlot slot) {
            
        }
    }
}