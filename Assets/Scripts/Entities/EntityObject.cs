using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    HOSTILE,
    FRIENDLY,
    NEUTRAL,
    PLAYER
}
[CreateAssetMenu(fileName = "New Entity", menuName = "Entity/Default Entity")]
public class EntityObject : ScriptableObject
{
    public string entityName;
    public int entityID;
    public EntityType entityType;

    [Header("Stats")]

    public float health;
    public float attack;
}

[System.Serializable]
public class Entity
{
    public string name;
    public int ID;
    public Entity(EntityObject entity)
    {
        name = entity.name;
        ID = entity.entityID;
    }
}
