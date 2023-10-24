using System;
using System.Collections;
using System.Collections.Generic;
using roguelike.core.item;
using UnityEngine;
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
        Add(Icon);
        Add(StackSize);

        Icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");

        RegisterCallback<PointerDownEvent>(OnPointerDown);

        UpdateSlotEvent += UpdateSlot;
    }

    private void OnPointerDown(PointerDownEvent evt) {

    }

    private void UpdateSlot() {
        Icon.image = SlotStack.Item.Icon.texture;
    }
    
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<ItemSlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}