using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using static Inventory;

public abstract class Container : MonoBehaviour
{
    private bool dragging = false;
    private List<GameObject> draggedSlots = new List<GameObject>();

    public InventoryObject inventory;
    public Dictionary<GameObject, ItemStack> itemsDisplayed = new Dictionary<GameObject, ItemStack>();

    

    void Start()
    {
        for(int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parentContainer = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
        }
        CreateSlots();
    }

    private void OnSlotUpdate(ItemStack _stack)
    {
        if (_stack.item.ID > 0)
        {
            _stack.displaySlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _stack.ItemObject.icon;
            _stack.displaySlot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _stack.displaySlot.GetComponentInChildren<TextMeshProUGUI>().text = _stack.itemAmount == 1 ? "" : _stack.itemAmount.ToString("n0");
        }
        else
        {
            _stack.displaySlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _stack.displaySlot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _stack.displaySlot.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    void Update()
    {
        if (MouseItem.gameObject != null)
        {
            MouseItem.gameObject.transform.SetParent(GameObject.Find("ItemHoverUI").transform);
            MoveItem();
        }
    }

    public abstract void CreateSlots();

    public void OnEnter(GameObject gameObject)
    {
        AddDraggingSlot(gameObject);
        MouseItem.hoverObject = gameObject;

        if (itemsDisplayed.ContainsKey(gameObject))
            MouseItem.hoverStack = itemsDisplayed[gameObject];
    }

    public void OnExit(GameObject gameObject)
    {
        AddDraggingSlot(gameObject);

        MouseItem.hoverObject = null;
        MouseItem.hoverStack = null;
    }

    public void OnBeginDrag(GameObject gameObject)
    {
        dragging = true;
    }

    public void OnStopDrag(GameObject gameObject)
    {
        dragging = false;
        MouseItem.itemStack.itemAmount = MouseItem.itemStack.itemAmount % draggedSlots.Count;
        if(MouseItem.itemStack.itemAmount == 0)
        {
            MouseItem.itemStack = null;
            Destroy(MouseItem.gameObject);
        }
        draggedSlots = new List<GameObject>();
    }
    
    public void MoveItem()
    {
        MouseItem.gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
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
        if (dragging && itemsDisplayed[gameObject].item.ID == 0 && !draggedSlots.Contains(gameObject) && MouseItem.itemStack != null && draggedSlots.Count < MouseItem.itemStack.itemAmount)
        {
            draggedSlots.Add(gameObject);
            draggedSlots.ForEach(SplitStack);
        }
    }

    void SplitStack(GameObject gameObject)
    {
        itemsDisplayed[gameObject].UpdateStack(MouseItem.itemStack.item, MouseItem.itemStack.itemAmount / draggedSlots.Count);
    }

}

public static class MouseItem
{
    public static GameObject gameObject;
    public static ItemStack itemStack;
    public static ItemStack hoverStack;
    public static GameObject hoverObject;
}