using System.Collections.Generic;
using UnityEngine;

public class DungeonStorage : MonoBehaviour
{
    public enum DungeonThemes
    {
        DEBUG,
        PLAGUE,
        ICE,
        CLASSIC
    }

    [SerializeField]
    public DungeonThemes selectedDungeonTheme = new DungeonThemes();

    [Header("Starting Rooms")]
    public GameObject[] spawnRooms;

    [Header("Ending Rooms")]
    public GameObject[] endRooms;


    [Header("Layouts")]
    public GameObject[] plagueLayouts;
    public GameObject[] iceLayouts;
    public GameObject[] classicLayouts;

    [Header("Rooms")]
    public GameObject[] plagueRooms;
    public GameObject[] iceRooms;
    public GameObject[] classicRooms;
}