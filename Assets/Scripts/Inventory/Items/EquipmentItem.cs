using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Items/Equippable Item")]

public class EquipmentItem : ItemObject
{
    public float attackDamage;
    void Awake()
    {
        type = ItemType.EQUIPMENT;
    }
}
