using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization;
using System.ComponentModel;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabase database;
    public Inventory inventory;
    public ItemStack[] GetSlots { get { return inventory.inventorySlots; } }
    public void AddItem(Item _item, int _itemAmount)
    {

        for(int i = 0; i < 24; i++)
        {
            if (GetSlots[i].item.ID == _item.ID)
            {
                if (GetSlots[i].item.maxStackSize < GetSlots[i].itemAmount + _itemAmount)
                {
                    int overflow = GetSlots[i].item.maxStackSize - GetSlots[i].itemAmount;
                    GetSlots[i].AddItemAmount(overflow);
                    _itemAmount -= overflow;

                } else if (GetSlots[i].item.maxStackSize > GetSlots[i].itemAmount + _itemAmount)
                {
                    GetSlots[i].AddItemAmount(_itemAmount);
                    return;
                }
            }
            
        }

        for (int i = 0; i < 24; i++)
        {
            if (GetSlots[i].item.ID == 0)
            {
                SetEmptySlot(_item, _itemAmount);
                return;
            }
        }

        Debug.Log("Found no valid slot!");
    }

    public ItemStack SetEmptySlot(Item _item, int itemAmount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.ID == 0)
            {
                GetSlots[i].UpdateStack(_item, itemAmount);
                return GetSlots[i];
            }
        }
        return null;
    }

    public void MoveItem(ItemStack item1, ItemStack item2)
    {
        ItemStack temp = new ItemStack(item1.item, item1.itemAmount);
        item1.UpdateStack(item2.item, item2.itemAmount);
        item2.UpdateStack(temp.item, temp.itemAmount);
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

    public void DropStack(ItemStack itemStack, GameObject placeOfSpawn)
    {
        if (itemStack == null || itemStack.item.ID == 0)
        {
            return;
        }

        var spawnedItem = new GameObject();
        var sphereCollider = spawnedItem.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        spawnedItem.AddComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, 0.5f);
        spawnedItem.transform.position = placeOfSpawn.transform.position;
        var itemEntity = spawnedItem.AddComponent<ItemEntity>();
        var rigidBody = spawnedItem.AddComponent<Rigidbody>();
        

        itemEntity.item = database.getItem[itemStack.item.ID];
        itemEntity.itemAmount = itemStack.itemAmount;
        itemStack.UpdateStack(new Item(), 0);

        rigidBody.AddExplosionForce(0.5f, placeOfSpawn.transform.position, 50f, 5f);
    }
    /*
    public void DropAll()
    {
        for (int i = 0; i < inventory.inventorySlots.Length; i++)
        {
            DropStack(i);
        }
    }
    */
}

public delegate void UpdateSlot(ItemStack _stack);

[System.Serializable]
public class ItemStack
{
    public SlotType[] allowedItems = new SlotType[0];

    [System.NonSerialized]
    public Container parentContainer;

    [System.NonSerialized]
    public GameObject displaySlot;

    [System.NonSerialized]
    public UpdateSlot OnAfterUpdate;

    [System.NonSerialized]
    public UpdateSlot OnBeforeUpdate;

    public Item item = new Item();
    public int itemAmount;

    public ItemObject ItemObject
    {
        get
        {
            if (item.ID >= 0)
            {
                return parentContainer.inventory.database.itemObjects[item.ID];
            }
            return null;
        }
    }

    public ItemStack()
    {
        UpdateStack(new Item(), 0);
    }

    public ItemStack(Item _item, int _itemAmount)
    {
        UpdateStack(_item, _itemAmount);
    }

    public void UpdateStack(Item _item, int _itemAmount)
    {
        if(OnBeforeUpdate != null)
        {
            OnBeforeUpdate.Invoke(this);
        }
        item = _item;
        itemAmount = _itemAmount;
        if(OnAfterUpdate != null)
        {
            OnAfterUpdate.Invoke(this);
        }
    }

    /// <summary>
    /// Add amount of items to the stack
    /// </summary>
    public void AddItemAmount(int amount)
    {
        UpdateStack(item, itemAmount += amount);
    }

    /// <summary>
    /// Check if an item can be placed in a slot
    /// </summary>
    public bool IsValidItem(ItemObject _itemObject)
    {
        if(allowedItems.Length <= 0)
        {
            return true;
        }
        for(int i = 0; i < allowedItems.Length; i++)
        {
            if (_itemObject.slotType == allowedItems[i])
            {
                return true;
            }
        }
        return false;
    } 
}

[System.Serializable]
public class Inventory
{
    public ItemStack[] inventorySlots = new ItemStack[36];

    public enum Slot
    {
        Helmet = 24,
        Chestplate = 25,
        Leggings = 26,
        Boots = 27,
        Primary = 28,
        Secondary = 29,
        Accessory = 30,
        Charm = 31,
        Belt = 32,
        Pouch = 33,
        Back = 34,
        Ring = 35
    }
}