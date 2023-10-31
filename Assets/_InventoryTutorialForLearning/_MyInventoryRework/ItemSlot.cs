using System;
using roguelike.core.item;
using roguelike.rendering.ui;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class ItemSlot : VisualElement {
    public Image Icon;
    public Label StackSize;

    public ItemStack SlotStack;

    public Action UpdateSlotEvent;

    public ItemSlot() {
        Icon = new Image();
        StackSize = new Label();
        Icon.Add(StackSize);
        Add(Icon);

        Icon.AddToClassList("slotIcon");
        StackSize.AddToClassList("stackSizeDisplay");
        AddToClassList("slotContainer");

        RegisterCallback<PointerDownEvent>(OnPointerDown);

        UpdateSlotEvent += UpdateSlot;
    }

    private void OnPointerDown(PointerDownEvent evt) {
        InventoryRenderer.ClickSlot(evt.position, this);
    }

    private void UpdateSlot() {
        Icon.image = SlotStack.Item.Icon.texture;
        StackSize.text = SlotStack.StackSize.ToString();
    }
    
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<ItemSlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}