
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public InventoryObject inventory;
    public GameObject inventoryPrefab;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    public Dictionary<GameObject, ItemStack> itemsDisplayed = new Dictionary<GameObject, ItemStack>();
    
    void Start()
    {
        CreateSlots();
    }
    void Update()
    {
        UpdateSlots();
        if (mouseItem.gameObject != null)
        {
            MoveItem();
        }
    }

    public void UpdateSlots()
    {
        foreach(KeyValuePair<GameObject, ItemStack> _slot in itemsDisplayed)
        {
            if(_slot.Value.itemID > 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[_slot.Value.item.ID].icon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.itemAmount == 1 ? "" : _slot.Value.itemAmount.ToString("n0");
            } else {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, ItemStack>();
        for (int i = 0; i < inventory.inventory.items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            /*
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { BeginDrag(obj); });
            
            AddEvent(obj, EventTriggerType.EndDrag, delegate { EndDrag(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            */

            //AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });

            itemsDisplayed.Add(obj, inventory.inventory.items[i]);
        }
    }

    public void OnEnter(GameObject gameObject)
    {
        mouseItem.hoverObject = gameObject;
        
        if(itemsDisplayed.ContainsKey(gameObject))
        {
            mouseItem.hoverStack = itemsDisplayed[gameObject];
        }
    }

    public void OnExit(GameObject gameObject)
    {
        mouseItem.hoverObject = null;
        mouseItem.hoverStack = null;
    }
    
    public void BeginDrag(GameObject gameObject)
    {
        var mouseObject = new GameObject();
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(30, 30);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[gameObject].itemID > 0)
        {
            var image = mouseObject.AddComponent<Image>();
            image.sprite = inventory.database.getItem[itemsDisplayed[gameObject].itemID].icon;
            image.raycastTarget = false;
        }
        mouseItem.gameObject = mouseObject;
        mouseItem.itemStack = itemsDisplayed[gameObject];
    }
    /*
    public void EndDrag(GameObject gameObject)
    {
        if (mouseItem.hoverObject)
        {
            inventory.MoveItem(itemsDisplayed[gameObject], itemsDisplayed[mouseItem.hoverObject]);
        } 
        else
        {
            Debug.Log("guh?");
        }
        Destroy(mouseItem.gameObject);
        mouseItem.itemStack = null;

    }

    public void OnDrag(GameObject gameObject)
    {
        if (mouseItem.gameObject != null)
        {
            mouseItem.gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    */
    public void OnClick(GameObject gameObject)
    {

        if (/*Input.GetMouseButton(0)*/ true)
        {
            ItemStack _itemStack;
            itemsDisplayed.TryGetValue(gameObject, out _itemStack);
            if (mouseItem.gameObject)
            {
                inventory.MoveItem(itemsDisplayed[gameObject], mouseItem.itemStack);
                mouseItem.itemStack = null;
                Destroy(mouseItem.gameObject);

            }
            else if (_itemStack.itemID > 0)
            {
                var mouseObject = new GameObject();
                var rectTransform = mouseObject.AddComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(30, 30);
                mouseObject.transform.SetParent(transform.parent);
                if (itemsDisplayed[gameObject].itemID > 0)
                {
                    var image = mouseObject.AddComponent<Image>();
                    image.sprite = inventory.database.getItem[itemsDisplayed[gameObject].itemID].icon;
                    image.raycastTarget = false;
                }
                Destroy(mouseItem.gameObject);
                mouseItem.itemStack = null;
                mouseItem.gameObject = mouseObject;
                mouseItem.itemStack = itemsDisplayed[gameObject];
            }
        }
        
    }

    public void MoveItem()
    {
        mouseItem.gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }

    private void AddEvent(GameObject gameObject, EventTriggerType triggerType, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = triggerType;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    
}

public class MouseItem
{
    public GameObject gameObject;
    public ItemStack itemStack;
    public ItemStack hoverStack;
    public GameObject hoverObject;
}
