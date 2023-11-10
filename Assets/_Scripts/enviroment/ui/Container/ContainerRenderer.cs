using System;
using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class ContainerRenderer {
        protected List<ItemSlot> inventorySlots = new List<ItemSlot>();

        public Action UpdateUIEvent;

        protected VisualElement _root, _inventoryRoot;
        protected ItemSlot _mouseSlot;

        protected Inventory _inventory;

        public ContainerRenderer(Inventory entityInventory, UIDocument inventoryUI) {
            _inventory = entityInventory;

            _root = inventoryUI.rootVisualElement;
            _inventoryRoot = _root.Q<VisualElement>("InventorySlotContainer");
            CreateSlots();

            _mouseSlot = _root.Q<ItemSlot>("MouseSlot");
            _mouseSlot.SetStack(ItemStack.EMPTY);
            _mouseSlot.Renderer = this;

            _root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        }

        // SLOT CREATION METHODS

        private void CreateSlots() {
            for(int i = 0; i < Inventory.InventorySize; i++) {
                ItemSlot itemSlot = new ItemSlot();
                itemSlot.SlotIndex = i;
                itemSlot.SetStack(_inventory.Items[i]);
                itemSlot.Renderer = this;
                inventorySlots.Add(itemSlot);
                _inventoryRoot.Add(itemSlot);
                itemSlot.UpdateSlotEvent.Invoke();
            }
        }



        // POINTER EVENT METHODS

        public void ClickSlot(Vector2 position, ItemSlot originalSlot, bool isPrimary) {

            if(_mouseSlot.SlotStack.IsEmpty() && originalSlot.SlotStack.IsEmpty()) {
                return;
            }

            _mouseSlot.style.top = position.y - _mouseSlot.layout.height / 2;
            _mouseSlot.style.left = position.x - _mouseSlot.layout.width / 2;

            ItemSlot clickedSlot;
            IEnumerable<ItemSlot> slots = inventorySlots.Where(x => x.worldBound.Overlaps(_mouseSlot.worldBound));

            if(slots.Count() > 0) {
                clickedSlot = slots.OrderBy(x => Vector2.Distance(x.worldBound.position, _mouseSlot.worldBound.position)).First();
            } else {
                clickedSlot = originalSlot;
            }

            if(clickedSlot.SlotStack.Item == _mouseSlot.SlotStack.Item &&
                clickedSlot.SlotStack.Item.MaxStackSize != 1) {
                FillSlot(clickedSlot);

                // todo: add logic for splitting stacks :3

            } else {
                SwapSlots(clickedSlot);
            }

            originalSlot = clickedSlot;
            originalSlot.UpdateSlotEvent.Invoke();

            UpdateSlots(clickedSlot);

            if(_mouseSlot.SlotStack.IsEmpty()) {
                _mouseSlot.style.visibility = Visibility.Hidden;
                _mouseSlot.style.top = 0;
                _mouseSlot.style.left = 0;
            } else {
                _mouseSlot.style.visibility = Visibility.Visible;
                _mouseSlot.UpdateSlotEvent.Invoke();
            }
        }

        protected void OnPointerMove(PointerMoveEvent evt) {
            if(_mouseSlot.style.visibility == Visibility.Hidden) {
                return;
            }
            _mouseSlot.style.top = evt.position.y - _mouseSlot.layout.height / 2;
            _mouseSlot.style.left = evt.position.x - _mouseSlot.layout.width / 2;
        }


        // SLOT INTERACTION METHODS

        /// <summary>
        /// Method that swaps the provided slot with the slot in hand.
        /// </summary>
        protected void SwapSlots(ItemSlot clickedSlot) {
            ItemStack tempStack = clickedSlot.SlotStack;
            if(clickedSlot.SetStack(_mouseSlot.SlotStack)) {
                _mouseSlot.SetStack(tempStack);
            }
        }

        /// <summary>
        /// Method that fills a clicked stack and subtracks from the mouse slot.
        /// </summary>
        protected void FillSlot(ItemSlot clickedSlot) {
            _mouseSlot.SlotStack.SetStackSize(clickedSlot.SlotStack.IncreaseStackSize(_mouseSlot.SlotStack.StackSize));
        }

        // todo: fix logic, it deletes items rn
        protected void SplitSlot(ItemSlot clickedSlot) {
            int split = clickedSlot.SlotStack.StackSize / 2;
            if(clickedSlot.SlotStack.StackSize % 2 == 1) { split++; }
            _mouseSlot.SetStack(clickedSlot.SlotStack);
            _mouseSlot.SlotStack.SetStackSize(split);
            clickedSlot.SlotStack.Decrease(split);
        }



        // MISC METHODS

        /// <summary>
        /// Method that syncs item slot that was clicked with it's internal counterpart.
        /// </summary>
        protected virtual void UpdateSlots(ItemSlot clickedSlot) {
            UpdateInventory(clickedSlot);
        }

        protected void UpdateInventory(ItemSlot clickedSlot) {
            int index = clickedSlot.SlotIndex;
            ItemStack stack = clickedSlot.SlotStack;
            if(!(_inventory.Items[index].IsEmpty() && stack.IsEmpty())) {
                _inventory.UpdateItemStack(stack, index);
            }
        }
    }
}
