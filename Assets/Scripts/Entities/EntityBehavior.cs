using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{
    public Behavior[] behavior = new Behavior[1];
}

[System.Serializable]
public class Behavior
{
    public BehaviorType behaviorType;
    [Range(0f, 100f)]
    public float intenfity;

    public enum BehaviorType
    {
        FOLLOW,
        WANDER,
        GUARD,
        IDLE
    }
}