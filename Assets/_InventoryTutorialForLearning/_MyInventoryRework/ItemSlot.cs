using System;
using roguelike.core.item;
using roguelike.rendering.ui;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class ItemSlot : VisualElement {
    private Image _icon;
    private Label _stackSize;
    private ItemStack _slotStack;

    public Action UpdateSlotEvent;

    public ItemStack SlotStack { get { return _slotStack; } }

    public ItemSlot() {
        _icon = new Image();
        _stackSize = new Label();
        _icon.Add(_stackSize);
        Add(_icon);

        _icon.AddToClassList("slotIcon");
        _stackSize.AddToClassList("stackSizeDisplay");
        AddToClassList("slotContainer");

        RegisterCallback<PointerDownEvent>(OnPointerDown);

        UpdateSlotEvent += UpdateSlot;
    }

    public void OnPointerDown(PointerDownEvent evt) {
        InventoryRenderer.ClickSlot(evt.position, this);
    }

    private void UpdateSlot() {
        _icon.image = _slotStack.Item.Icon.texture;
        _stackSize.text = _slotStack.StackSize > 1 ? _slotStack.StackSize.ToString() : "";
    }

    public void SetStack(ItemStack stack) {
        _slotStack = stack;
        UpdateSlotEvent.Invoke();
    }
    
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<ItemSlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}