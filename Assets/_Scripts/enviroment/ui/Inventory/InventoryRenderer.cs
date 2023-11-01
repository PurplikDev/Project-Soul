using System;
using System.Collections.Generic;
using roguelike.core.item;
using roguelike.core.utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class InventoryRenderer {

        private List<ItemSlot> inventorySlots = new List<ItemSlot>();

        public Action UpdateUIEvent;

        private VisualElement _root, _inventoryAnchor;
        private static ItemSlot _mouseSlot;

        public InventoryRenderer(Inventory playerInventory, UIDocument inventoryUI) {
            _root = inventoryUI.rootVisualElement;
            _inventoryAnchor = _root.Q<VisualElement>("InventorySlotContainer");

            createSlots(playerInventory);

            inventorySlots.ForEach(delegate (ItemSlot slot) {
                slot.UpdateSlotEvent.Invoke();
            });

            _mouseSlot = _root.Q<ItemSlot>("MouseSlot");
            _mouseSlot.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _mouseSlot.SlotStack = ItemStack.EMPTY;
        }

        private void createSlots(Inventory inventory) {
            foreach(ItemStack stack in inventory.Items) {
                ItemSlot itemSlot = new ItemSlot();
                itemSlot.SlotStack = stack;
                itemSlot.RegisterCallback<PointerDownEvent>(itemSlot.OnPointerDown);
                inventorySlots.Add(itemSlot);
                _inventoryAnchor.Add(itemSlot);
            }
        }

        public static void ClickSlot(Vector2 position, ItemSlot originalSlot) {

            if(originalSlot.SlotStack.IsEmpty()) {
                Debug.Log("Clicked slot is empty!");
            } else {
                Debug.Log("Clicked slot contains " + originalSlot.SlotStack.Item.Name);
            }


            // this has issues, need to fix the math
            if(originalSlot.SlotStack.Item != _mouseSlot.SlotStack.Item) {
                SwapSlots(originalSlot);
            } else {
                FillSlot(originalSlot);
            }



            originalSlot.UpdateSlotEvent.Invoke();

            if(_mouseSlot.SlotStack.IsEmpty()) {
                _mouseSlot.style.visibility = Visibility.Hidden;
            } else {
                _mouseSlot.style.visibility = Visibility.Visible;
                _mouseSlot.UpdateSlotEvent.Invoke();
            }
        }

        private static void SwapSlots(ItemSlot clickedSlot) {
            var tempStack = _mouseSlot.SlotStack;
            _mouseSlot.SlotStack = clickedSlot.SlotStack;
            clickedSlot.SlotStack = tempStack;
        }

        private static void FillSlot(ItemSlot clickedSlot) {
            int value = Mathematicus.OverflowFromAddition(
                clickedSlot.SlotStack.StackSize,
                _mouseSlot.SlotStack.StackSize,
                clickedSlot.SlotStack.Item.MaxStackSize);
            if(value == 0) {
                clickedSlot.SlotStack.IncreaseStack(_mouseSlot.SlotStack.StackSize);
                _mouseSlot.SlotStack = ItemStack.EMPTY;
            } else {
                clickedSlot.SlotStack.IncreaseStack(_mouseSlot.SlotStack.StackSize);
                int value2 = _mouseSlot.SlotStack.StackSize - value;
                _mouseSlot.SlotStack.IncreaseStack(-1 * value);
            }
        }

        private void OnPointerMove(PointerMoveEvent evt) {
            if(_mouseSlot.style.visibility == Visibility.Hidden) {
                return;
            }
            _mouseSlot.style.top = evt.position.y - _mouseSlot.layout.height / 2;
            _mouseSlot.style.left = evt.position.x - _mouseSlot.layout.width / 2;
        }
    }
}