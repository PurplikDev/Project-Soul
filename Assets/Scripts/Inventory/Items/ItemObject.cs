using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum ItemType
{
    GENERIC,
    MATERIAL,
    CONSUMABLE,
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
    BAUBLE
}

public abstract class ItemObject : ScriptableObject
{
    public int ID;
    public Sprite icon;
    public ItemType type;
    public SlotType slotType;
    [Range(1, 32)]
    public int maxStackSize = 32;

    [TextArea(15,20)]
    public string desc;
}

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public int ID;
    public int maxStackSize;
    public Item(ItemObject item)
    {
        name = item.name;
        description = item.desc;
        ID = item.ID;
        maxStackSize = item.maxStackSize;
    }
}


