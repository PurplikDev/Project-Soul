using System;
using roguelike.core.item;
using roguelike.rendering.ui;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class ItemSlot : VisualElement {
    public Image Icon;
    private Label _stackSize;

    public ItemStack SlotStack;

    public Action UpdateSlotEvent;

    public ItemSlot() {
        Icon = new Image();
        _stackSize = new Label();
        Icon.Add(_stackSize);
        Add(Icon);

        Icon.AddToClassList("slotIcon");
        _stackSize.AddToClassList("stackSizeDisplay");
        AddToClassList("slotContainer");

        UpdateSlotEvent += UpdateSlot;
    }

    public void OnPointerDown(PointerDownEvent evt) {
        InventoryRenderer.ClickSlot(evt.position, this);
    }

    private void UpdateSlot() {
        Icon.image = SlotStack.Item.Icon.texture;
        _stackSize.text = SlotStack.StackSize > 1 ? SlotStack.StackSize.ToString() : "";
    }
    
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<ItemSlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}