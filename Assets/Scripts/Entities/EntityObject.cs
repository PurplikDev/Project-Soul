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

    [TextArea(15, 20)]
    public string description;

    [Header("Default Entity Stats")]
    public int health = 20;
    public int attack = 4;
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
