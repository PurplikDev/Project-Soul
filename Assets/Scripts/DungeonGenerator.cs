using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    DungeonStorage.DungeonThemes selectedDungeonTheme = new DungeonStorage.DungeonThemes();

    private void Start()
    {
        
        Instantiate(transform.GetComponent<DungeonStorage>().spawnRooms[selectedDungeonTheme], transform.position, Quaternion.identity);
    }
}
