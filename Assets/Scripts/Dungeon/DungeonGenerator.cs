using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public Transform dungeonGenerationController;
    private DungeonStorage dungeonStorage;

    public Dictionary<DungeonStorage.DungeonThemes, GameObject> spawnRooms = new Dictionary<DungeonStorage.DungeonThemes, GameObject>();
    public bool hasExit;
    public int amountOfExits;
   

    private void Awake()
    {
        dungeonStorage = dungeonGenerationController.GetComponent<DungeonStorage>();

        
        spawnRooms.Add(DungeonStorage.DungeonThemes.PLAGUE, dungeonStorage.spawnRooms[0]);
        //spawnRooms.Add(DungeonStorage.DungeonThemes.ICE, dungeonStorage.spawnRooms[1]);
        //spawnRooms.Add(DungeonStorage.DungeonThemes.CLASSIC, dungeonStorage.spawnRooms[2]);
        //spawnRooms.Add(DungeonStorage.DungeonThemes.DEBUG, dungeonStorage.spawnRooms[3]);

        Instantiate(spawnRooms[dungeonStorage.selectedDungeonTheme], transform.position, transform.rotation);
    }
}
