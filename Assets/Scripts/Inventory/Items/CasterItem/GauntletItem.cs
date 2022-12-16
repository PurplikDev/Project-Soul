using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Items/Equipment/Caster/Gauntlet")]

public class GauntletItem : CasterItem
{
    [System.NonSerialized]
    public CasterType casterType = CasterType.GAUNTLET;

    [Header("Gauntlet Parts")]

    public CorePart corePart;
    public ConductorPart conductorPart;

    public enum CorePart
    {
        LEATHER,

    }

    public enum ConductorPart
    {
        IRON,
        BRASS
    }

    void Awake()
    {
        type = ItemType.EQUIPMENT;
    }
}
