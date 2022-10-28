using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public InventoryObject inventory;

    void OnTriggerEnter(Collider collider)
    {
        var item = collider.GetComponent<ItemEntity>();

        if(item)
        {
            inventory.AddItem(new Item(item.item), 1);
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
        inventory.inventory.items.Clear();
    }
}
