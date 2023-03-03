using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : LivingEntity
{
    [Header("Class Stats")]
    public int rogueStat = 0;
    public int thaumaturgeStat = 0;
    public int templarStat = 0;

    [Header("Madness")]
    public int madness;

    [Header("Inventory")]
    public InventoryObject inventory;

    [Header("Keybinds")]
    public KeybindManager keybindManager;
}
