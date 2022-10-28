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

public abstract class ItemObject : ScriptableObject
{
    public int ID;
    public Sprite icon;
    public ItemType type;

    [TextArea(15,20)]
    public string desc;
}

[System.Serializable]
public class Item
{
    public string name;
    public int ID;
    public Item(ItemObject item)
    {
        name = item.name;
        ID = item.ID;
    }
}


