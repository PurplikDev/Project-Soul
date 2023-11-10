using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using roguelike.enviroment.world.deployable.workstation;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class CraftingRenderer : ContainerRenderer {
        private VisualElement _craftingRoot;

        private List<ItemSlot> _craftingSlots = new List<ItemSlot>();

        private CraftingStation _station;

        public CraftingRenderer(Inventory entityInventory, UIDocument inventoryUI, CraftingStation station) : base(entityInventory, inventoryUI) {
            _craftingRoot = _root.Q<VisualElement>("CraftingSlotContainer");
            _station = station;

            RegisterCraftingSlots();
        }



        // SLOT REGISTRATION METHODS

        private void RegisterCraftingSlots() {
            int index = 0;
            foreach(ItemSlot slot in _craftingRoot.Children().ToList()) {
                RegisterSlot(slot, _station.StationInventory[index]);
                index++;
            }
            ItemSlot resultSlot = _root.Q<ItemSlot>("ResultSlot");
            RegisterSlot(resultSlot, _station.ResultStack);
        }

        private void RegisterSlot(ItemSlot slot, ItemStack stack) {
            slot.SetStack(stack);
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
                ItemStack stack = clickedSlot.SlotStack;
                int workingIndex = clickedSlot.SlotIndex - Inventory.InventorySize;

                if(clickedSlot is ResultSlot) {
                    _station.ResultStack = stack;
                }

                if(!(_station.StationInventory[workingIndex].IsEmpty() && stack.IsEmpty()) || clickedSlot is ResultSlot) {
                    _station.StationInventory[workingIndex] = stack;
                }

                _station.SlotUpdateEvent.Invoke();
            }
        }
    }
}