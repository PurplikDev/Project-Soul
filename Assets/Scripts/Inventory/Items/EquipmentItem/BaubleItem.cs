using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Items/Equipment/Bauble")]

public class BaubleItem : EquipmentItem
{
    public BaubleType baubleType;

    public enum BaubleType
    {
        ACCESSORY,
        CHARM,
        RING,
        GLOVE,
        BELT,
        POUCH,
        BACK
    }

    void Awake()
    {
        type = ItemType.EQUIPMENT;
    }
}
