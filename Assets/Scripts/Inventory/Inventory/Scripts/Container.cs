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

    public GameObject tooltipPrefab;

    private bool dragging = false;
    private List<GameObject> draggedSlots = new List<GameObject>();

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
        AddDraggingSlot(gameObject);
        player.mouseItem.hoverObject = gameObject;

        if (itemsDisplayed.ContainsKey(gameObject))
            player.mouseItem.hoverStack = itemsDisplayed[gameObject];
    }

    public void OnExit(GameObject gameObject)
    {
        AddDraggingSlot(gameObject);

        player.mouseItem.hoverObject = null;
        player.mouseItem.hoverStack = null;
    }

    public void OnBeginDrag(GameObject gameObject)
    {
        dragging = true;
    }

    public void OnStopDrag(GameObject gameObject)
    {
        dragging = false;
        player.mouseItem.itemStack.itemAmount = player.mouseItem.itemStack.itemAmount % draggedSlots.Count;
        if(player.mouseItem.itemStack.itemAmount == 0)
        {
            player.mouseItem.itemStack = null;
            Destroy(player.mouseItem.gameObject);
        }
        draggedSlots = new List<GameObject>();
    }
    
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

    private void AddDraggingSlot(GameObject gameObject)
    {
        if (dragging && itemsDisplayed[gameObject].itemID == 0 && !draggedSlots.Contains(gameObject) && player.mouseItem.itemStack != null && draggedSlots.Count < player.mouseItem.itemStack.itemAmount)
        {
            draggedSlots.Add(gameObject);
            draggedSlots.ForEach(SplitStack);
        }
    }

    void SplitStack(GameObject gameObject)
    {
        itemsDisplayed[gameObject].UpdateStack(player.mouseItem.itemStack.itemID, player.mouseItem.itemStack.item, player.mouseItem.itemStack.itemAmount / draggedSlots.Count);
    }

}

public class MouseItem
{
    public GameObject gameObject;
    public ItemStack itemStack;
    public ItemStack hoverStack;
    public GameObject hoverObject;
}
