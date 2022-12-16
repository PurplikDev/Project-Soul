using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : ItemObject
{
    void Awake()
    {
        type = ItemType.EQUIPMENT;
    }
}
