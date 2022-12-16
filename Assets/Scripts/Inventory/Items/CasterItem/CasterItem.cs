using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterItem : EquipmentItem
{
    [Header("Caster Stats")]

    public float speed;
    public int storage;
    public float discount;

    public enum CasterType
    {
        WAND,
        GAUNTLET,
        STAF
    }

    void Awake()
    {
        type = ItemType.EQUIPMENT;
    }
}
