using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Items/Consumable Item")]

public class ConsumableItem : ItemObject
{
    [Header("Health Restore")]
    public int healthRestore;
    void Awake()
    {
        type = ItemType.CONSUMABLE;
    }
}
