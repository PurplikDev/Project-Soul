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

        protected VisualElement root, inventoryRoot;
        protected ItemSlot mouseSlot;
        protected Inventory inventory;

        public ContainerRenderer(Inventory entityInventory, UIDocument inventoryUI) {
            inventory = entityInventory;

            root = inventoryUI.rootVisualElement;

            inventoryRoot = root.Q<VisualElement>("InventorySlotContainer");

            StyleColor imageTint = new StyleColor();
            imageTint.value = new Color(255, 255, 255, 0);

            mouseSlot = root.Q<ItemSlot>("MouseSlot");
            mouseSlot.SetStack(ItemStack.EMPTY);
            mouseSlot.Renderer = this;
            mouseSlot.style.unityBackgroundImageTintColor = imageTint;

            root.RegisterCallback<PointerMoveEvent>(OnPointerMove);

            RegisterItemSlots();
        }

        protected void RegisterItemSlots() {
            foreach(ItemSlot itemSlot in inventoryRoot.Children().ToList()) {
                itemSlot.SlotIndex = itemSlots.Count;
                itemSlot.SetStack(inventory.Items[itemSlot.SlotIndex]);
                itemSlot.Renderer = this;
                itemSlots.Add(itemSlot);
                itemSlot.UpdateSlotEvent.Invoke();
            }
        }

        // POINTER EVENT METHODS

        public virtual void ClickSlot(Vector2 position, ItemSlot originalSlot, bool isPrimary) {

            if (mouseSlot.SlotStack.IsEmpty() && originalSlot.SlotStack.IsEmpty()) {
                return;
            }

            mouseSlot.style.top = position.y - mouseSlot.layout.height / 2;
            mouseSlot.style.left = position.x - mouseSlot.layout.width / 2;

            ItemSlot clickedSlot;
            IEnumerable<ItemSlot> slots = itemSlots.Where(x => x.worldBound.Overlaps(mouseSlot.worldBound));

            if (slots.Count() > 0) {
                clickedSlot = slots.OrderBy(x => Vector2.Distance(x.worldBound.position, mouseSlot.worldBound.position)).First();
            } else {
                clickedSlot = originalSlot;
            }

            if (clickedSlot.SlotStack.Item == mouseSlot.SlotStack.Item &&
                clickedSlot.SlotStack.Item.MaxStackSize != 1 && isPrimary) {
                FillSlot(clickedSlot);

                // todo: add logic for splitting stacks :3
            } else if (clickedSlot.SlotStack.StackSize > 1 && mouseSlot.SlotStack.IsEmpty() && !isPrimary) {
                SplitSlot(clickedSlot);
            } else {
                SwapSlots(clickedSlot);
            }

            originalSlot = clickedSlot;
            originalSlot.UpdateSlotEvent.Invoke();

            SyncVisualToInternalSingle(clickedSlot);

            if(mouseSlot.SlotStack.IsEmpty()) {
                mouseSlot.style.visibility = Visibility.Hidden;
                mouseSlot.style.top = 0;
                mouseSlot.style.left = 0;
            } else {
                mouseSlot.style.visibility = Visibility.Visible;
                mouseSlot.UpdateSlotEvent.Invoke();
            }
        }

        protected void OnPointerMove(PointerMoveEvent evt) {
            if(mouseSlot.style.visibility == Visibility.Hidden) {
                return;
            }
            mouseSlot.style.top = evt.position.y - mouseSlot.layout.height / 2;
            mouseSlot.style.left = evt.position.x - mouseSlot.layout.width / 2;
        }


        // SLOT INTERACTION METHODS

        /// <summary>
        /// Method that swaps the provided slot with the slot in hand.
        /// </summary>
        protected void SwapSlots(ItemSlot clickedSlot) {
            ItemStack tempStack = clickedSlot.SlotStack;
            if(clickedSlot.SetStack(mouseSlot.SlotStack)) {
                mouseSlot.SetStack(tempStack);
            }
        }

        /// <summary>
        /// Method that fills a clicked stack and subtracks from the mouse slot.
        /// </summary>
        protected void FillSlot(ItemSlot clickedSlot) {
            mouseSlot.SlotStack.SetStackSize(clickedSlot.SlotStack.IncreaseStackSize(mouseSlot.SlotStack.StackSize));
        }

        protected void SplitSlot(ItemSlot clickedSlot) {
            int split = clickedSlot.SlotStack.StackSize / 2;
            mouseSlot.SetStack(new ItemStack(clickedSlot.SlotStack.Item, split));
            clickedSlot.SlotStack.DecreaseStackSize(split);
        }



        // MISC METHODS

        protected abstract void SyncVisualToInternalSingle(ItemSlot clickedSlot); // Updates Internal inventory to be like visual

        protected void TranslateHeader(Label label) {
            label.text = TranslationManager.getTranslation(label.text);
        }
    }
}
