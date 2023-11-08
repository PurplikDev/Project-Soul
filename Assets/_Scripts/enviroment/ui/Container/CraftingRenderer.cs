using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using roguelike.enviroment.entity.player;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class CraftingRenderer : ContainerRenderer {
        private VisualElement _craftingRoot;

        public List<ItemStack> _craftingStacks;
        private List<ItemSlot> _craftingSlots = new List<ItemSlot>();

        public CraftingRenderer(Inventory entityInventory, UIDocument inventoryUI, params ItemStack[] craftingStacks) : base(entityInventory, inventoryUI) {
            _craftingRoot = _root.Q<VisualElement>("CraftingSlotContainer");

            _craftingStacks = craftingStacks.ToList();

            RegisterCraftingSlots();
        }



        // SLOT REGISTRATION METHODS

        private void RegisterCraftingSlots() {
            int index = 0;
            foreach(ItemSlot slot in _craftingRoot.Children().ToList()) {
                RegisterSlot(slot, index);
                index++;
            }
            ItemSlot resultSlot = _root.Q<ItemSlot>("ResultSlot");
            RegisterSlot(resultSlot, index);
        }

        private void RegisterSlot(ItemSlot slot, int index) {
            slot.SetStack(_craftingStacks[index]);
            slot.Renderer = this;
            slot.SlotIndex = inventorySlots.Count();
            _craftingSlots.Add(slot);
            inventorySlots.Add(slot);
        }

        // Inventory Sync

        protected override void UpdateSlots(ItemSlot clickedSlot) {
            if(clickedSlot.SlotIndex < Inventory.InventorySize) {
                base.UpdateSlots(clickedSlot);
            } else {
                // todo: proper logic
            }
        }
    }
}