using System;
using System.Collections.Generic;
using roguelike.core.item;
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
        }

        private void createSlots(Inventory inventory) {
            foreach(ItemStack stack in inventory.Items) {
                ItemSlot itemSlot = new ItemSlot();
                itemSlot.SlotStack = stack;
                inventorySlots.Add(itemSlot);
                _inventoryAnchor.Add(itemSlot);
            }
        }

        public static void ClickSlot(Vector2 position, ItemSlot originalSlot) {

            var clickedSlot = originalSlot;

            if(originalSlot.SlotStack.Item == ItemManager.GetItemByID("cum3")) {
                originalSlot.SlotStack.IncreaseStack(1);
                _mouseSlot.style.visibility = Visibility.Hidden;
            } else {
                originalSlot.SlotStack = new ItemStack(ItemManager.GetItemByID("cum3"));
                _mouseSlot.SlotStack = clickedSlot.SlotStack;
                _mouseSlot.style.visibility = Visibility.Visible;
            }

            _mouseSlot.style.top = position.y - _mouseSlot.layout.height / 2;
            _mouseSlot.style.left = position.x - _mouseSlot.layout.width / 2;

            originalSlot.UpdateSlotEvent.Invoke();
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