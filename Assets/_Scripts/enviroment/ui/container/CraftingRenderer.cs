using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using roguelike.enviroment.world.deployable.workstation;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class CraftingRenderer : DeployableRenderer {
        private VisualElement _craftingSlotsRoot;
        private ResultSlot _resultSlot;

        private List<ItemSlot> _craftingSlots = new List<ItemSlot>();

        public CraftingRenderer(Inventory interactorInventory, CraftingStation deployable, UIDocument inventoryUI) : base(interactorInventory, deployable, inventoryUI) {
            _craftingSlotsRoot = _root.Q<VisualElement>("CraftingSlotContainer");
            _resultSlot = _root.Q<ResultSlot>("ResultSlot");

            RegisterDeployableSlots();
        }

        protected override void RegisterDeployableSlots() {
            int index = 0;
            foreach(ItemSlot slot in _craftingSlotsRoot.Children().ToList()) {
                RegisterSlot(slot, deployable.StationInventory[index]);
                index++;
            }
            RegisterSlot(_resultSlot, ((CraftingStation)deployable).ResultStack);
        }

        private void RegisterSlot(ItemSlot slot, ItemStack stack) {
            slot.SetStack(stack);
            slot.Renderer = this;
            slot.SlotIndex = itemSlots.Count;
            _craftingSlots.Add(slot);
            itemSlots.Add(slot);
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            if(clickedSlot.SlotIndex < Inventory.InventorySize) {
                _inventory.Items[clickedSlot.SlotIndex] = clickedSlot.SlotStack;
            } else {
                ItemStack stack = clickedSlot.SlotStack;
                int workingIndex = clickedSlot.SlotIndex - Inventory.InventorySize;

                if(clickedSlot is ResultSlot) {
                    ((CraftingStation)deployable).RecipeTakenEvent.Invoke();
                    return;
                }

                if(!(deployable.StationInventory[workingIndex].IsEmpty() && stack.IsEmpty())) {
                    deployable.StationInventory[workingIndex] = stack;
                }

                var recipe = ((CraftingStation)deployable).CheckForRecipes();

                if(recipe != null) {
                    _resultSlot.ForceStack(recipe.Result);
                } else {
                    _resultSlot.ForceStack(ItemStack.EMPTY);
                }
            }
        }
    }
}