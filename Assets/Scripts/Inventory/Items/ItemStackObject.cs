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
        displayInventory.itemsDisplayed.TryGetValue(gameObject, out _itemStack);
        if (eventData.button == PointerEventData.InputButton.Left) {
            
            if (displayInventory.mouseItem.gameObject)
            {
                displayInventory.inventory.MoveItem(displayInventory.mouseItem.itemStack, displayInventory.itemsDisplayed[gameObject]);
                if (displayInventory.mouseItem.itemStack.itemID == 0)
                {
                    Destroy(displayInventory.mouseItem.gameObject);
                    displayInventory.mouseItem.itemStack = null;
                }
                else
                {
                    UpdateMouseObject(displayInventory.mouseItem.gameObject);
                }
            }
            else if (_itemStack.itemID > 0)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject);
                displayInventory.mouseItem.itemStack = null;
                displayInventory.mouseItem.gameObject = mouseObject;
                displayInventory.mouseItem.itemStack = new ItemStack(displayInventory.itemsDisplayed[gameObject].itemID, displayInventory.itemsDisplayed[gameObject].item, displayInventory.itemsDisplayed[gameObject].itemAmount);
                _itemStack.UpdateStack(0, null, 0);
            }
        } else if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (_itemStack.itemID > 0 && !displayInventory.mouseItem.gameObject && _itemStack.itemAmount > 1)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject);
                displayInventory.mouseItem.itemStack = null;
                displayInventory.mouseItem.gameObject = mouseObject;
                displayInventory.mouseItem.itemStack = new ItemStack(_itemStack.itemID, _itemStack.item, _itemStack.itemAmount / 2);
                _itemStack.itemAmount -= displayInventory.mouseItem.itemStack.itemAmount;
            } else if (_itemStack.itemID > 0 && !displayInventory.mouseItem.gameObject && _itemStack.itemAmount == 1)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject);
                displayInventory.mouseItem.itemStack = null;
                displayInventory.mouseItem.gameObject = mouseObject;
                displayInventory.mouseItem.itemStack = displayInventory.itemsDisplayed[gameObject];
            }
        }
    }

    public void InstatiateMouseObject(GameObject mouseObject )
    {
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(30, 30);
        mouseObject.transform.SetParent(transform.parent);
        if (displayInventory.itemsDisplayed[gameObject].itemID > 0)
        {
            var image = mouseObject.AddComponent<Image>();
            image.sprite = displayInventory.inventory.database.getItem[displayInventory.itemsDisplayed[gameObject].itemID].icon;
            image.raycastTarget = false;
        }
        Destroy(displayInventory.mouseItem.gameObject);
    }

    public void UpdateMouseObject(GameObject mouseObject)
    {
        var image = mouseObject.GetComponent<Image>();
        image.sprite = displayInventory.inventory.database.getItem[displayInventory.mouseItem.itemStack.itemID].icon;
    }
}