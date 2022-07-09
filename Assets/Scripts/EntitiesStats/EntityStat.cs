using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerStat
{
    [SerializeField]
    private int baseValue;

    public float GetValue() {
        return baseValue;
    }
}
