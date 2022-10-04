using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    DungeonStorage.DungeonThemes selectedDungeonTheme = new DungeonStorage.DungeonThemes();

    public bool start;

    void Update()
    {
        while(start)
        {
            Instantiate(transform.GetComponent<DungeonStorage>().spawnRooms[selectedDungeonTheme], transform.position, Quaternion.identity);
            start = false;
        }
    }
}
