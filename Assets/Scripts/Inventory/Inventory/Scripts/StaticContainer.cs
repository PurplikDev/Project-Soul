using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticContainer : Container
{
    public GameObject[] slots;
    public override void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, ItemStack>();

        for(int i = 0; i < inventory.inventory.inventorySlots.Length; i++)
        {
            var obj = slots[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            inventory.GetSlots[i].displaySlot = obj;
            itemsDisplayed.Add(obj, inventory.GetSlots[i]);
        }
    }
}
