using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestPlayer : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    public InventoryObject inventory;

    void OnTriggerEnter(Collider collider)
    {
        var item = collider.GetComponent<ItemEntity>();
        int itemAmount = collider.GetComponent<ItemEntity>().itemAmount;

        if(item)
        {
            inventory.AddItem(new Item(item.item), itemAmount);
            Destroy(collider.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SaveInventory();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.LoadInventory();
        }
    }

    void OnApplicationQuit()
    {
        inventory.inventory.items = new ItemStack[28];
    }
}
