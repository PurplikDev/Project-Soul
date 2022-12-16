using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Items/Equipment/Armor")]

public class ArmorItem : EquipmentItem
{
    public ArmorType armorType;
    public int armorValue;

    public enum ArmorType
    {
        HELMET,
        CHESTPLATE,
        LEGGINGS,
        BOOTS
    }

    void Awake()
    {
        type = ItemType.EQUIPMENT;
    }
}
