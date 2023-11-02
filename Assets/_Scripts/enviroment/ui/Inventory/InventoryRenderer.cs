using System;
using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class InventoryRenderer {

        private static List<ItemSlot> inventorySlots = new List<ItemSlot>();

        public Action UpdateUIEvent;

        private VisualElement _root, _inventoryAnchor;
        private static ItemSlot _mouseSlot;

        private static Inventory _inventory;

        public InventoryRenderer(Inventory playerInventory, UIDocument inventoryUI) {
            _inventory = playerInventory;

            _root = inventoryUI.rootVisualElement;
            _inventoryAnchor = _root.Q<VisualElement>("InventorySlotContainer");

            createSlots(playerInventory);

            _mouseSlot = _root.Q<ItemSlot>("MouseSlot");
            _mouseSlot.SetStack(ItemStack.EMPTY);

            _root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        }

        private void createSlots(Inventory inventory) {
            foreach(ItemStack stack in inventory.Items) {
                ItemSlot itemSlot = new ItemSlot();
                itemSlot.SetStack(stack);
                itemSlot.RegisterCallback<PointerDownEvent>(itemSlot.OnPointerDown);
                inventorySlots.Add(itemSlot);
                _inventoryAnchor.Add(itemSlot);
                itemSlot.UpdateSlotEvent.Invoke();
            }
        }

        public static void ClickSlot(Vector2 position, ItemSlot originalSlot) {

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
            } else {
                SwapSlots(clickedSlot);
            }


            originalSlot = clickedSlot;
            originalSlot.UpdateSlotEvent.Invoke();

            // todo: LINK VISUAL SLOTS TO ACTUAL ITEM STACKS IN THE INVENTORY
            //       AND MOVE ITEMS AROUND CORRECTLY :|

            if(_mouseSlot.SlotStack.IsEmpty()) {
                _mouseSlot.style.visibility = Visibility.Hidden;
                _mouseSlot.style.top = 0;
                _mouseSlot.style.left = 0;
            } else {
                _mouseSlot.style.visibility = Visibility.Visible;
                _mouseSlot.UpdateSlotEvent.Invoke();
            }
        }

        private static void SwapSlots(ItemSlot clickedSlot) {
            var tempStack = _mouseSlot.SlotStack;
            _mouseSlot.SetStack(clickedSlot.SlotStack);
            clickedSlot.SetStack(tempStack);
        }

        private static void FillSlot(ItemSlot clickedSlot) {
            _mouseSlot.SlotStack.SetStackSize(clickedSlot.SlotStack.IncreaseStackSize(_mouseSlot.SlotStack.StackSize));
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