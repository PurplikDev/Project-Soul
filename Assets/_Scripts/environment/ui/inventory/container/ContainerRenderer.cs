using System;
using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using roguelike.rendering.ui.slot;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public abstract class ContainerRenderer {
        internal List<ItemSlot> itemSlots = new List<ItemSlot>();

        public Action UpdateUIEvent;

        protected VisualElement _root, _inventoryRoot;
        protected ItemSlot _mouseSlot;
        protected Inventory _inventory;

        public ContainerRenderer(Inventory entityInventory, UIDocument inventoryUI) {
            _inventory = entityInventory;

            _root = inventoryUI.rootVisualElement;

            _inventoryRoot = _root.Q<VisualElement>("InventorySlotContainer");

            StyleColor imageTint = new StyleColor();
            imageTint.value = new Color(255, 255, 255, 0);

            _mouseSlot = _root.Q<ItemSlot>("MouseSlot");
            _mouseSlot.SetStack(ItemStack.EMPTY);
            _mouseSlot.Renderer = this;
            _mouseSlot.style.unityBackgroundImageTintColor = imageTint;

            _root.RegisterCallback<PointerMoveEvent>(OnPointerMove);

            RegisterItemSlots();
        }

        protected void RegisterItemSlots() {
            foreach(ItemSlot itemSlot in _inventoryRoot.Children().ToList()) {
                itemSlot.SlotIndex = itemSlots.Count;
                itemSlot.SetStack(_inventory.Items[itemSlot.SlotIndex]);
                itemSlot.Renderer = this;
                itemSlots.Add(itemSlot);
                itemSlot.UpdateSlotEvent.Invoke();
            }
        }

        // POINTER EVENT METHODS

        public virtual void ClickSlot(Vector2 position, ItemSlot originalSlot, bool isPrimary) {

            if (_mouseSlot.SlotStack.IsEmpty() && originalSlot.SlotStack.IsEmpty()) {
                return;
            }

            _mouseSlot.style.top = position.y - _mouseSlot.layout.height / 2;
            _mouseSlot.style.left = position.x - _mouseSlot.layout.width / 2;

            ItemSlot clickedSlot;
            IEnumerable<ItemSlot> slots = itemSlots.Where(x => x.worldBound.Overlaps(_mouseSlot.worldBound));

            if (slots.Count() > 0) {
                clickedSlot = slots.OrderBy(x => Vector2.Distance(x.worldBound.position, _mouseSlot.worldBound.position)).First();
            } else {
                clickedSlot = originalSlot;
            }

            if (clickedSlot.SlotStack.Item == _mouseSlot.SlotStack.Item &&
                clickedSlot.SlotStack.Item.MaxStackSize != 1 && isPrimary) {
                FillSlot(clickedSlot);

                // todo: add logic for splitting stacks :3
            } else if (clickedSlot.SlotStack.StackSize > 1 && _mouseSlot.SlotStack.IsEmpty() && !isPrimary) {
                SplitSlot(clickedSlot);
            } else {
                SwapSlots(clickedSlot);
            }

            originalSlot = clickedSlot;
            originalSlot.UpdateSlotEvent.Invoke();

            SyncVisualToInternalSingle(clickedSlot);

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

        protected void SplitSlot(ItemSlot clickedSlot) {
            int split = clickedSlot.SlotStack.StackSize / 2;
            _mouseSlot.SetStack(new ItemStack(clickedSlot.SlotStack.Item, split));
            clickedSlot.SlotStack.DecreaseStackSize(split);
        }



        // MISC METHODS

        protected abstract void SyncVisualToInternalSingle(ItemSlot clickedSlot); // Updates Internal inventory to be like visual

        protected void TranslateHeader(Label label) {
            label.text = TranslationManager.getTranslation(label.text);
        }
    }
}
