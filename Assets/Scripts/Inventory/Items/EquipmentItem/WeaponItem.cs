using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Items/Equipment/Weapon")]

public class WeaponItem : EquipmentItem
{
    public WeaponType weaponType;

    [Header("Weapon Stats")]

    public float damage;
    public float speed;
    public float cutting;

    public enum WeaponType
    {
        SWORD,
        DAGGER
    }

    void Awake()
    {
        type = ItemType.EQUIPMENT;
    }
}
