using System;
using roguelike.core.item;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.slot {
    public class ItemSlot : VisualElement {
        protected Image _icon;
        protected Label _stackSize;
        protected ItemStack _slotStack;

        public int SlotIndex;

        public Action UpdateSlotEvent;

        public ItemStack SlotStack { get { return _slotStack; } }

        public ContainerRenderer Renderer;

        public ItemSlot() {
            _stackSize = new Label();
            _icon = new Image();

            _icon.Add(_stackSize);
            Add(_icon);

            _icon.AddToClassList("slotIcon");
            _stackSize.AddToClassList("stackSizeDisplay");
            AddToClassList("slotContainer");

            RegisterCallback<PointerDownEvent>(OnPointerDown);

            UpdateSlotEvent += UpdateSlot;
        }

        public void OnPointerDown(PointerDownEvent evt) {
            Renderer.ClickSlot(evt.position, this, evt.pressedButtons == 1);
        }

        protected virtual void UpdateSlot() {
            _icon.image = _slotStack.Item.Icon.texture;
            _stackSize.text = _slotStack.StackSize > 1 ? _slotStack.StackSize.ToString() : "";
        }

        public virtual bool SetStack(ItemStack stack) {
            _slotStack = stack;
            UpdateSlotEvent.Invoke();
            return true;
        }

        #region UXML
        [Preserve]
        public new class UxmlFactory : UxmlFactory<ItemSlot, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}