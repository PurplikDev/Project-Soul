using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization;
using System.ComponentModel;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabase database;
    public Inventory inventory;

    public void AddItem(Item _item, int _itemAmount)
    {

        for(int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].item.ID == _item.ID)
            {
                if (inventory.items[i].item.maxStackSize < inventory.items[i].itemAmount + _itemAmount)
                {
                    int overflow = inventory.items[i].item.maxStackSize - inventory.items[i].itemAmount;
                    inventory.items[i].AddItemAmount(overflow);
                    _itemAmount -= overflow;

                } else if (inventory.items[i].item.maxStackSize > inventory.items[i].itemAmount + _itemAmount)
                {
                    inventory.items[i].AddItemAmount(_itemAmount);
                    return;
                }
            }
            
        }
        SetEmptySlot(_item, _itemAmount);

    }

    public ItemStack SetEmptySlot(Item _item, int itemAmount)
    {
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].itemID == 0)
            {
                inventory.items[i].UpdateStack(_item.ID, _item, itemAmount);
                return inventory.items[i];
            }
        }
        return null;
    }

    public void MoveItem(ItemStack item1, ItemStack item2)
    {
        ItemStack temp = new ItemStack(item1.itemID, item1.item, item1.itemAmount);
        item1.UpdateStack(item2.itemID, item2.item, item2.itemAmount);
        item2.UpdateStack(temp.itemID, temp.item, temp.itemAmount);
    }


    [ContextMenu("Save Inventory")]
    public void SaveInventory()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, inventory);
        stream.Close();
    }

    [ContextMenu("Load Inventory")]
    public void LoadInventory()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            inventory = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    [ContextMenu("Clear Inventory")]
    public void ClearInventory()
    {
        inventory = new Inventory();
    }
}

[System.Serializable]
public class ItemStack
{
    public Container parentContainer;
    public int itemID;
    public Item item;
    public int itemAmount;
    public ItemStack()
    {
        itemID = 0;
        item = null;
        itemAmount = 0;
    }

    public ItemStack(int _itemID, Item _item, int _itemAmount)
    {
        itemID = _itemID;
        item = _item;
        itemAmount = _itemAmount;
    }

    public void UpdateStack(int _itemID, Item _item, int _itemAmount)
    {
        itemID = _itemID;
        item = _item;
        itemAmount = _itemAmount;
    }

    /// <summary>
    /// Add amount of items to the stack
    /// </summary>
    public void AddItemAmount(int amount)
    {
        itemAmount += amount;
    }
}

[System.Serializable]
public class Inventory
{
    public ItemStack[] items = new ItemStack[28];
}