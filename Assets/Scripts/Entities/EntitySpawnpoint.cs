using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnpoint : MonoBehaviour
{
    public EntityType entityTypeToSpawn;

    public enum EntityType
    {
        PLAYER
    }

    void Start()
    {
        switch (entityTypeToSpawn)
        {
            case EntityType.PLAYER:

                break;
        }
    }
}
