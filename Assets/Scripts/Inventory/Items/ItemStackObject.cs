using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemStackObject : MonoBehaviour, IPointerClickHandler
{
    private Container container;
    private void Awake()
    {
        container = transform.GetComponentInParent<Container>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemStack _itemStack;
        var mouseItem = container.player.mouseItem;
        container.itemsDisplayed.TryGetValue(gameObject, out _itemStack);

        // Check for left mouse button click
        if (eventData.button == PointerEventData.InputButton.Left) {
            // Check if player is already holding an item
            if (mouseItem.gameObject)
            {
                // If an item's assigned slot type does not correspond to the slot nothing happens
                if (!_itemStack.IsValidItem(container.inventory.database.getItem[mouseItem.itemStack.itemID])) {
                    return;
                }

                container.inventory.MoveItem(mouseItem.itemStack, container.itemsDisplayed[gameObject]);

                // Reset player holding an item if it is empty (literally deleting air lol)
                if (mouseItem.itemStack.itemID == 0)
                {
                    Destroy(mouseItem.gameObject);
                    mouseItem.itemStack = null;
                } else 
                // If held item and the hovering item stack is the same item the items the player is holding will be added
                if (mouseItem.itemStack.itemID == _itemStack.itemID)
                {
                    _itemStack.AddItemAmount(mouseItem.itemStack.itemAmount);

                    // If player overflows the maxStackSize of an item the overflow is deposited back into player's "hand"
                    if (_itemStack.itemAmount > _itemStack.item.maxStackSize)
                    {
                        int overflow = _itemStack.itemAmount - _itemStack.item.maxStackSize;
                        mouseItem.itemStack.itemAmount = overflow;
                        _itemStack.itemAmount = _itemStack.item.maxStackSize;
                    } 
                    else // Reset player's "hand"
                    {
                        mouseItem.itemStack = null;
                        Destroy(mouseItem.gameObject);
                    }
                } else
                {
                    UpdateMouseObject(mouseItem.gameObject);
                }
            } 
            else 
            // If player's "hand" is empty and the selected item isn't "air" player "grabs" the item
            if (_itemStack.itemID > 0)
            {
                var mouseObject = new GameObject();
                var clickedItem = container.itemsDisplayed[gameObject];
                InstatiateMouseObject(mouseObject);
                mouseItem.itemStack = null;
                mouseItem.gameObject = mouseObject;
                mouseItem.itemStack = new ItemStack(clickedItem.itemID, clickedItem.item, clickedItem.itemAmount);
                _itemStack.UpdateStack(0, null, 0);
            }
        } else
        // Check for right mouse button click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // If player's "hand" is empty and the itemStack isn't air it gets split into two stacks one of which is "grabbed" by the player
            if (_itemStack.itemID > 0 && !mouseItem.gameObject && _itemStack.itemAmount > 1)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject);
                mouseItem.itemStack = null;
                mouseItem.gameObject = mouseObject;
                mouseItem.itemStack = new ItemStack(_itemStack.itemID, _itemStack.item, _itemStack.itemAmount / 2);
                _itemStack.itemAmount -= mouseItem.itemStack.itemAmount;
            }
            // If player tries to split an itemStack that is not splittable into two (single item) it executes normal "grabbing" of an item
            else if (_itemStack.itemID > 0 && !mouseItem.gameObject && _itemStack.itemAmount == 1)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject);
                mouseItem.itemStack = null;
                mouseItem.gameObject = mouseObject;
                mouseItem.itemStack = new ItemStack(container.itemsDisplayed[gameObject].itemID, container.itemsDisplayed[gameObject].item, container.itemsDisplayed[gameObject].itemAmount);
                _itemStack.UpdateStack(0, null, 0);

            } else 
            // If a player is "holding" an item and left clicked itemStack matches the item in player's hand it deposits one item into the stack
            if (( _itemStack.itemID == mouseItem.itemStack.itemID && _itemStack.itemAmount < _itemStack.item.maxStackSize) || _itemStack.itemID == 0)
            {
                mouseItem.itemStack.AddItemAmount(-1);
                if (_itemStack.itemID == 0)
                {
                    _itemStack.UpdateStack(mouseItem.itemStack.itemID, mouseItem.itemStack.item, 1);
                }
                else {
                    _itemStack.AddItemAmount(1);
                }
                if(mouseItem.itemStack.itemAmount == 0)
                {
                    mouseItem.itemStack = null;
                    Destroy(mouseItem.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Creates item icon around player's cursor
    /// </summary>
    public void InstatiateMouseObject(GameObject mouseObject )
    {
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        var mouseItem = container.player.mouseItem;
        rectTransform.sizeDelta = new Vector2(30, 30);
        mouseObject.transform.SetParent(transform.parent.parent);
        if (container.itemsDisplayed[gameObject].itemID > 0)
        {
            var image = mouseObject.AddComponent<Image>();
            image.sprite = container.inventory.database.getItem[container.itemsDisplayed[gameObject].itemID].icon;
            image.raycastTarget = false;
        }
        Destroy(mouseItem.gameObject);
    }

    public void UpdateMouseObject(GameObject mouseObject)
    {
        var image = mouseObject.GetComponent<Image>();
        var mouseItem = container.player.mouseItem;
        image.sprite = container.inventory.database.getItem[mouseItem.itemStack.itemID].icon;
        if(mouseItem.itemStack.itemAmount == 0)
        {
            Destroy(mouseObject);
        }
    }
}