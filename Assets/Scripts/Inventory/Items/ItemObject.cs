using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum ItemType
{
    GENERIC,
    EQUIPMENT
}

public enum SlotType
{
    GENERIC,
    HELMET,
    CHESTPLATE,
    LEGGINGS,
    BOOTS,
    WEAPON,
    ACCESSORY,
    CHARM,
    BELT,
    POUCH,
    BACK,
    RING
}

public abstract class ItemObject : ScriptableObject
{
    [Header("Item Details")]
    public int ID;
    public Sprite icon;
    public GameObject itemModel;

    [TextArea(15, 20)]
    public string description;

    [Header("Item Type")]
    public ItemType type;

    [Header("Slot Type")]
    public SlotType slotType;
    [Header("Item Stack Size")]
    [Range(1, 32)]
    public int maxStackSize = 32;
}

[System.Serializable]
public class Item
{
    public string name;
    public int ID;
    public int maxStackSize;

    public Item()
    {
        name = "air";
        ID = 0;
    }

    public Item(ItemObject item)
    {
        name = item.name;
        ID = item.ID;
        maxStackSize = item.maxStackSize;
    }
}


