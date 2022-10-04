using System.Collections.Generic;
using UnityEngine;

public class DungeonStorage : MonoBehaviour
{
    public enum DungeonThemes
    {
        DEBUG,
        PLAGUE,

    }

    public GameObject[] inputSpawnRooms;
    public Dictionary<DungeonThemes, GameObject> spawnRooms = new Dictionary<DungeonThemes, GameObject>();

    void Awake()
    {
        spawnRooms.Add(DungeonThemes.DEBUG, inputSpawnRooms[0]);
        spawnRooms.Add(DungeonThemes.PLAGUE, inputSpawnRooms[1]);

    }
}
