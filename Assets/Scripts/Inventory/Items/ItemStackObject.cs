using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemStackObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Container container;
    private ItemStack _itemStack;

    private void Awake()
    {
        container = transform.GetComponentInParent<Container>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            container.itemsDisplayed.TryGetValue(gameObject, out _itemStack);
            container.inventory.DropStack(_itemStack, container.player);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        container.itemsDisplayed.TryGetValue(gameObject, out _itemStack);

        // Check for left mouse button click
        if (eventData.button == PointerEventData.InputButton.Left) {
            // Check if player is already holding an item
            if (MouseItem.gameObject)
            {
                // If an item's assigned slot type does not correspond to the slot nothing happens
                if (!_itemStack.IsValidItem(container.inventory.database.getItem[MouseItem.itemStack.item.ID])) {
                    return;
                }

                container.inventory.MoveItem(MouseItem.itemStack, container.itemsDisplayed[gameObject]);

                // Reset player holding an item if it is empty (literally deleting air lol)
                if (MouseItem.itemStack.item.ID == 0)
                {
                    Destroy(MouseItem.gameObject);
                    MouseItem.itemStack = null;
                } else 
                // If held item and the hovering item stack is the same item the items the player is holding will be added
                if (MouseItem.itemStack.item.ID == _itemStack.item.ID)
                {
                    _itemStack.AddItemAmount(MouseItem.itemStack.itemAmount);

                    // If player overflows the maxStackSize of an item the overflow is deposited back into player's "hand"
                    if (_itemStack.itemAmount > _itemStack.item.maxStackSize)
                    {
                        int overflow = _itemStack.itemAmount - _itemStack.item.maxStackSize;
                        MouseItem.itemStack.itemAmount = overflow;
                        _itemStack.UpdateStack(_itemStack.item, _itemStack.item.maxStackSize);
                    } 
                    else // Reset player's "hand"
                    {
                        MouseItem.itemStack = null;
                        Destroy(MouseItem.gameObject);
                    }
                } else
                {
                    UpdateMouseObject(MouseItem.gameObject);
                }
            } 
            else 
            // If player's "hand" is empty and the selected item isn't "air" player "grabs" the item
            if (_itemStack.item.ID > 0)
            {
                var mouseObject = new GameObject();
                var clickedItem = container.itemsDisplayed[gameObject];
                InstatiateMouseObject(mouseObject, _itemStack);
                MouseItem.itemStack = null;
                MouseItem.gameObject = mouseObject;
                MouseItem.itemStack = new ItemStack(clickedItem.item, clickedItem.itemAmount);
                _itemStack.UpdateStack(new Item(), 0);
            }
        } else
        // Check for right mouse button click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // If player's "hand" is empty and the itemStack isn't air it gets split into two stacks one of which is "grabbed" by the player
            if (_itemStack.item.ID > 0 && !MouseItem.gameObject && _itemStack.itemAmount > 1)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject, _itemStack);
                MouseItem.itemStack = null;
                MouseItem.gameObject = mouseObject;
                MouseItem.itemStack = new ItemStack(_itemStack.item, _itemStack.itemAmount / 2);
                _itemStack.UpdateStack(_itemStack.item, _itemStack.itemAmount -= MouseItem.itemStack.itemAmount);
            }
            // If player tries to split an itemStack that is not splittable into two (single item) it executes normal "grabbing" of an item
            else if (_itemStack.item.ID > 0 && !MouseItem.gameObject && _itemStack.itemAmount == 1)
            {
                var mouseObject = new GameObject();
                InstatiateMouseObject(mouseObject, _itemStack);
                MouseItem.itemStack = null;
                MouseItem.gameObject = mouseObject;
                MouseItem.itemStack = new ItemStack(container.itemsDisplayed[gameObject].item, container.itemsDisplayed[gameObject].itemAmount);
                _itemStack.UpdateStack(null, 0);

            } else 
            // If a player is "holding" an item and left clicked itemStack matches the item in player's hand it deposits one item into the stack
            if ((_itemStack.item.ID == MouseItem.itemStack.item.ID && _itemStack.itemAmount < _itemStack.item.maxStackSize) || _itemStack.item.ID == 0)
            {
                MouseItem.itemStack.AddItemAmount(-1);
                if (_itemStack.item.ID == 0)
                {
                    _itemStack.UpdateStack(MouseItem.itemStack.item, 1);
                }
                else {
                    _itemStack.AddItemAmount(1);
                }
                if(MouseItem.itemStack.itemAmount == 0)
                {
                    MouseItem.itemStack = null;
                    Destroy(MouseItem.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Creates item icon around player's cursor
    /// </summary>
    public void InstatiateMouseObject(GameObject mouseObject, ItemStack _itemStack)
    {
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width / 25, Screen.width / 25);
        mouseObject.name = "MouseObject";
        mouseObject.layer = 5;
        if (container.itemsDisplayed[gameObject].item.ID > 0)
        {
            var image = mouseObject.AddComponent<Image>();
            image.sprite = container.inventory.database.getItem[container.itemsDisplayed[gameObject].item.ID].icon;
            image.raycastTarget = false;
        }
        Destroy(MouseItem.gameObject);
    }

    public void UpdateMouseObject(GameObject mouseObject)
    {
        var image = mouseObject.GetComponent<Image>();
        image.sprite = container.inventory.database.getItem[MouseItem.itemStack.item.ID].icon;
        if(MouseItem.itemStack.itemAmount == 0)
        {
            Destroy(mouseObject);
        }
    }
    /*
    //This updated the tooltip's position
    void UpdateTooltip()
    {
        if (tooltip == null) return;
        tooltip.transform.position = Input.mousePosition + new Vector3(
            (tooltip.transform.GetComponent<RectTransform>().rect.width / 2) + emptySampleStack.GetComponent<RectTransform>().rect.width / 2,
            ((tooltip.transform.GetComponent<RectTransform>().rect.height / 2) + emptySampleStack.GetComponent<RectTransform>().rect.width / 2) * -1, 0);
    }
    */
    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemStack _itemStack;
        container.itemsDisplayed.TryGetValue(gameObject, out _itemStack);
        /*
        if (_itemStack.itemID <= 0 || tooltip != null || container.player.mouseItem.itemStack != null) return;
        var tooltipObject = Instantiate(tooltipPrefab, new Vector3(-9999, -9999, -9999), Quaternion.identity, transform);
        tooltipObject.name = "Item Tooltip";
        tooltipObject.transform.SetParent(transform.parent);

        tooltip = tooltipObject.gameObject;
        tooltip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _itemStack.item.name;
        Debug.Log(tooltip.transform.GetChild(0).name);
        tooltip.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _itemStack.item.description;
        */
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        /*
        if (!eventData.pointerCurrentRaycast.gameObject.transform.IsChildOf(transform) && tooltip != null) { 
            Destroy(tooltip);
            tooltip = null;
        }
        */
    }
}