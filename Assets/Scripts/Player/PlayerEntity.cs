using UnityEngine;
using io.purplik.ProjectSoul.InventorySystem;

public class PlayerEntity : LivingEntity
{
    [Header("Class Stats")]
    public int rogueStat = 0;
    public int thaumaturgeStat = 0;
    public int templarStat = 0;

    [Header("Madness")]
    public int madness;
}