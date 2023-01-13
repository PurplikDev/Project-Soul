using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class Container : MonoBehaviour
{
    public TestPlayer player;

    public InventoryObject inventory;
    public Dictionary<GameObject, ItemStack> itemsDisplayed = new Dictionary<GameObject, ItemStack>();

    void Start()
    {
        for(int i = 0; i < inventory.inventory.items.Length; i++)
        {
            inventory.inventory.items[i].parentContainer = this;
        }
        CreateSlots();
    }
    void Update()
    {
        UpdateSlots();
        if (player.mouseItem.gameObject != null)
        {
            MoveItem();
        }
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, ItemStack> _slot in itemsDisplayed)
        {
            if (_slot.Value.itemID > 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[_slot.Value.item.ID].icon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.itemAmount == 1 ? "" : _slot.Value.itemAmount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public abstract void CreateSlots();

    public void OnEnter(GameObject gameObject)
    {
        player.mouseItem.hoverObject = gameObject;
        if (itemsDisplayed.ContainsKey(gameObject))
            player.mouseItem.hoverStack = itemsDisplayed[gameObject];
    }

    public void OnExit(GameObject gameObject)
    {
        player.mouseItem.hoverObject = null;
        player.mouseItem.hoverStack = null;
    }
    /*
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
        player.mouseItem.gameObject = mouseObject;
        player.mouseItem.itemStack = itemsDisplayed[gameObject];
    }
    
    public void OnClick(GameObject gameObject)
    {
            ItemStack _itemStack;
            itemsDisplayed.TryGetValue(gameObject, out _itemStack);
            if (player.mouseItem.gameObject)
            {
                inventory.MoveItem(itemsDisplayed[gameObject], player.mouseItem.itemStack);
            player.mouseItem.itemStack = null;
                Destroy(player.mouseItem.gameObject);

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
                Destroy(player.mouseItem.gameObject);
                player.mouseItem.itemStack = null;
                player.mouseItem.gameObject = mouseObject;
                player.mouseItem.itemStack = itemsDisplayed[gameObject];
        }

    }
    */
    public void MoveItem()
    {
        player.mouseItem.gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    protected void AddEvent(GameObject gameObject, EventTriggerType triggerType, UnityAction<BaseEventData> action)
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
