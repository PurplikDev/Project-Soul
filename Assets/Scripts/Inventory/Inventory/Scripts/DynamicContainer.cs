using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicContainer : Container
{
    public GameObject inventoryPrefab;

    public float X_START;
    public float Y_START;
    public float X_SPACE_BETWEEN_ITEM;
    public float NUMBER_OF_COLUMN;
    public float Y_SPACE_BETWEEN_ITEMS;

    public override void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, ItemStack>();
        for (int i = 0; i < inventory.inventory.inventorySlots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            /*
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnBeginDrag(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnStopDrag(obj); });
            */
            itemsDisplayed.Add(obj, inventory.GetSlots[i]);
        }
    }
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * Mathf.Floor(i / NUMBER_OF_COLUMN)), 0f);
    }
}
