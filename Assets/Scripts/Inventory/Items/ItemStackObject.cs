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
                displayInventory.inventory.MoveItem(displayInventory.itemsDisplayed[gameObject], displayInventory.mouseItem.itemStack);
                displayInventory.mouseItem.itemStack = null;
                Destroy(displayInventory.mouseItem.gameObject);

            }
            else if (_itemStack.itemID > 0)
            {
                var mouseObject = new GameObject();
                InstatiateHoverObject(mouseObject);
                displayInventory.mouseItem.itemStack = null;
                displayInventory.mouseItem.gameObject = mouseObject;
                displayInventory.mouseItem.itemStack = displayInventory.itemsDisplayed[gameObject];
            }
        } else if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (_itemStack.itemID > 0 && !displayInventory.mouseItem.gameObject && _itemStack.itemAmount > 1)
            {
                var mouseObject = new GameObject();
                InstatiateHoverObject(mouseObject);
                displayInventory.mouseItem.itemStack = null;
                displayInventory.mouseItem.gameObject = mouseObject;
                displayInventory.mouseItem.itemStack = new ItemStack(_itemStack.itemID, _itemStack.item, _itemStack.itemAmount / 2);
                _itemStack.itemAmount -= displayInventory.mouseItem.itemStack.itemAmount;
            } else if (_itemStack.itemID > 0 && !displayInventory.mouseItem.gameObject && _itemStack.itemAmount == 1)
            {
                var mouseObject = new GameObject();
                InstatiateHoverObject(mouseObject);
                displayInventory.mouseItem.itemStack = null;
                displayInventory.mouseItem.gameObject = mouseObject;
                displayInventory.mouseItem.itemStack = displayInventory.itemsDisplayed[gameObject];
            }
        }
    }

    public void InstatiateHoverObject(GameObject mouseObject )
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
}