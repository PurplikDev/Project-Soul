using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Items/Equipment/Shield")]

public class ShieldItem : EquipmentItem
{
    [Header("Shield Stats")]
    public float block;
    public float speed;

    void Awake()
    {
        type = ItemType.EQUIPMENT;
    }
}
