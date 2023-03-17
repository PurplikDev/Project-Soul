using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Camera entityCamera;
    public Behavior[] behavior = new Behavior[1];

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        entityCamera.farClipPlane = GetComponent<LivingEntity>().visionRange;
    }
}

[System.Serializable]
public class Behavior
{
    public BehaviorType behaviorType;
    [Range(0f, 100f)]
    public float intensity;

    public enum BehaviorType
    {
        IDLE,
        FOLLOW,
        WANDER,
        GUARD
    }
}