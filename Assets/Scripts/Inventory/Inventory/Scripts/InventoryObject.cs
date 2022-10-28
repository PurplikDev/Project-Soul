using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabase database;
    public Inventory inventory;

    public void AddItem(Item _item, int itemAmount)
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].item.ID == _item.ID)
            {
                inventory.items[i].AddItemAmount(itemAmount);
                return;
            }
        }
        inventory.items.Add(new ItemStack(_item.ID, _item, itemAmount));

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
    public int itemID;
    public Item item;
    public int itemAmount;
    public ItemStack(int _itemID, Item _item, int _itemAmount)
    {
        itemID = _itemID;
        item = _item;
        itemAmount = _itemAmount;
    }

    public void AddItemAmount(int amount)
    {
        itemAmount += amount;
    }
}

[System.Serializable]
public class Inventory
{
    public List<ItemStack> items = new List<ItemStack>();
}