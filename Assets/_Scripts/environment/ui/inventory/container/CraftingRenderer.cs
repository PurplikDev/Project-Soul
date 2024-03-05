using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using roguelike.environment.world.deployable.workstation;
using roguelike.rendering.ui.slot;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class CraftingRenderer : DeployableRenderer {
        private VisualElement _craftingSlotsRoot;
        private ResultSlot _resultSlot;

        private List<ItemSlot> _craftingSlots = new List<ItemSlot>();

        public CraftingRenderer(Inventory interactorInventory, CraftingStation deployable, UIDocument inventoryUI) : base(interactorInventory, deployable, inventoryUI) {
            _craftingSlotsRoot = root.Q<VisualElement>("CraftingSlotContainer");
            _resultSlot = root.Q<ResultSlot>("ResultSlot");

            TranslationManager.TranslateHeader(root.Q<Label>("InventoryHeader"));
            TranslationManager.TranslateHeader(root.Q<Label>("CraftingHeader"));

            RegisterDeployableSlots();
        }

        protected override void RegisterDeployableSlots() {
            int index = 0;
            foreach(ItemSlot slot in _craftingSlotsRoot.Children().ToList()) {
                _craftingSlots.Add(RegisterSlot(slot, deployable.StationInventory[index]));
                index++;
            }
            RegisterSlot(_resultSlot, ((CraftingStation)deployable).ResultStack);
        }

        private ItemSlot RegisterSlot(ItemSlot slot, ItemStack stack) {
            slot.SetStack(stack);
            slot.Renderer = this;
            slot.SlotIndex = itemSlots.Count;
            itemSlots.Add(slot);
            return slot;
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            if(clickedSlot.SlotIndex < Inventory.InventorySize) {
                inventory.Items[clickedSlot.SlotIndex] = clickedSlot.SlotStack;
            } else {
                ItemStack stack = clickedSlot.SlotStack;
                int workingIndex = clickedSlot.SlotIndex - Inventory.InventorySize;

                if(clickedSlot is ResultSlot) {
                    ((CraftingStation)deployable).RecipeTakenEvent.Invoke();
                    SyncCraftingSlots();
                }

                if(!(clickedSlot is ResultSlot) && !(deployable.StationInventory[workingIndex].IsEmpty() && stack.IsEmpty())) {
                    deployable.StationInventory[workingIndex] = stack;
                }

                var recipe = ((CraftingStation)deployable).CheckForRecipes();

                if(recipe != null) {
                    Debug.Log("recipe is null");
                    _resultSlot.ForceStack(recipe.Result);
                } else {
                    _resultSlot.ForceStack(ItemStack.EMPTY);
                }
            }
        }

        private void SyncCraftingSlots() {
            foreach(ItemSlot slot in _craftingSlots) {
                int workingIndex = slot.SlotIndex - Inventory.InventorySize;
                slot.SetStack(deployable.StationInventory[workingIndex]);
            }
        }
    }
}