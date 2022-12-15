using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemStackObject : MonoBehaviour, IPointerClickHandler
{
    private DisplayInventory displayInventory;
    private void Awake()
    {
        displayInventory = transform.GetComponentInParent<DisplayInventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemStack _itemStack;
        var mouseItem = displayInventory.mouseItem;
        displayInventory.itemsDisplayed.TryGetValue(gameObject, out _itemStack);
        if (eventData.button == PointerEventData.InputButton.Left) {
            
            if (mouseItem.gameObject)
            {
                displayInventory.inventory.MoveItem(mouseItem.itemStack, displayInventory.itemsDisplayed[gameObject]);
                if (mouseItem.itemStack.itemID == 0)
                {
                    Destroy(mouseItem.gameObject);
                    mouseItem.itemStack = null;
                } else if (mouseItem.itemStack.itemID == _itemStack.itemID)
                {
                    _itemStack.AddItemAmount(mouseItem.itemStack.itemAmount);
                    if(_itemStack.itemAmount > _itemStack.item.maxStackSize)
                    {
                        int overflow = _itemStack.itemAmount - _itemStack.item.maxStackSize;
                        mouseItem.itemStack.itemAmount = overflow;
                        _itemStack.itemAmount = _itemStack.item.maxStackSize;
                    } else
                    {
                        mouseItem.itemStack = null;
                        Destroy(mouseItem.gameObject);
                    }
                } else
                {
                    UpdateMouseObject(mouseItem.gameObject);
                }
            }
            else if (_itemStack.itemID > 0)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject);
                mouseItem.itemStack = null;
                mouseItem.gameObject = mouseObject;
                mouseItem.itemStack = new ItemStack(displayInventory.itemsDisplayed[gameObject].itemID, displayInventory.itemsDisplayed[gameObject].item, displayInventory.itemsDisplayed[gameObject].itemAmount);
                _itemStack.UpdateStack(0, null, 0);
            }
        } else if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (_itemStack.itemID > 0 && !mouseItem.gameObject && _itemStack.itemAmount > 1)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject);
                mouseItem.itemStack = null;
                mouseItem.gameObject = mouseObject;
                mouseItem.itemStack = new ItemStack(_itemStack.itemID, _itemStack.item, _itemStack.itemAmount / 2);
                _itemStack.itemAmount -= mouseItem.itemStack.itemAmount;
            } else if (_itemStack.itemID > 0 && !mouseItem.gameObject && _itemStack.itemAmount == 1)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject);
                mouseItem.itemStack = null;
                mouseItem.gameObject = mouseObject;
                mouseItem.itemStack = displayInventory.itemsDisplayed[gameObject];
            } else if(_itemStack.itemID == mouseItem.itemStack.itemID && _itemStack.itemAmount < _itemStack.item.maxStackSize)
            {
                _itemStack.AddItemAmount(1);
                mouseItem.itemStack.AddItemAmount(-1);
                if(mouseItem.itemStack.itemAmount == 0)
                {
                    mouseItem.itemStack = null;
                    Destroy(mouseItem.gameObject);
                }
            }
        }
    }

    public void InstatiateMouseObject(GameObject mouseObject )
    {
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        var mouseItem = displayInventory.mouseItem;
        rectTransform.sizeDelta = new Vector2(30, 30);
        mouseObject.transform.SetParent(transform.parent);
        if (displayInventory.itemsDisplayed[gameObject].itemID > 0)
        {
            var image = mouseObject.AddComponent<Image>();
            image.sprite = displayInventory.inventory.database.getItem[displayInventory.itemsDisplayed[gameObject].itemID].icon;
            image.raycastTarget = false;
        }
        Destroy(mouseItem.gameObject);
    }

    public void UpdateMouseObject(GameObject mouseObject)
    {
        var image = mouseObject.GetComponent<Image>();
        var mouseItem = displayInventory.mouseItem;
        image.sprite = displayInventory.inventory.database.getItem[mouseItem.itemStack.itemID].icon;
        if(mouseItem.itemStack.itemAmount == 0)
        {
            Destroy(mouseObject);
        }
    }
}