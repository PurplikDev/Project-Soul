using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using roguelike.environment.world.deployable.workstation;
using roguelike.rendering.ui.slot;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class StorageCrateRenderer : DeployableRenderer {

        private List<ItemSlot> _storageSlots = new List<ItemSlot>();
        private VisualElement _storageSlotsRoot;

        public StorageCrateRenderer(Inventory interactorInventory, StorageCrate deployable, UIDocument inventoryUI) : base(interactorInventory, deployable, inventoryUI) {

            _storageSlotsRoot = root.Q<VisualElement>("StorageSlotContainer");

            TranslateHeader(root.Q<Label>("InventoryHeader"));
            TranslateHeader(root.Q<Label>("StorageHeader"));            

            RegisterDeployableSlots();
        }

        protected override void RegisterDeployableSlots() {
            int index = 0;
            foreach(ItemSlot slot in _storageSlotsRoot.Children().ToList()) {
                RegisterSlot(slot, deployable.StationInventory[index]);
                index++;
            }
        }

        private void RegisterSlot(ItemSlot slot, ItemStack stack) {
            slot.SetStack(stack);
            slot.Renderer = this;
            slot.SlotIndex = itemSlots.Count;
            _storageSlots.Add(slot);
            itemSlots.Add(slot);
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            if(clickedSlot.SlotIndex < Inventory.InventorySize) {
                inventory.Items[clickedSlot.SlotIndex] = clickedSlot.SlotStack;
            } else {
                ItemStack stack = clickedSlot.SlotStack;
                int workingIndex = clickedSlot.SlotIndex - Inventory.InventorySize;

                deployable.StationInventory[workingIndex] = stack;
            }
        }
    }
}