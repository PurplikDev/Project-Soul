using System.Collections.Generic;
using UnityEngine;
using static DungeonSpawnPoint;

public class DungeonStorage : MonoBehaviour
{
    public enum DungeonThemes
    {
        DEBUG,
        PLAGUE,
        ICE,
        CLASSIC
    }

    public enum GeneratorType
    {
        LAYOUT,
        ROOM,
        CORRIDOR,
        ENDROOM
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

    public Dictionary<DungeonThemes, Dictionary<GeneratorType, GameObject[]>> themeTypes = new Dictionary<DungeonThemes, Dictionary<GeneratorType, GameObject[]>>();

    public Dictionary<GeneratorType, GameObject[]> plagueParts = new Dictionary<GeneratorType, GameObject[]>();
    public Dictionary<GeneratorType, GameObject[]> iceParts = new Dictionary<GeneratorType, GameObject[]>();
    public Dictionary<GeneratorType, GameObject[]> classicParts = new Dictionary<GeneratorType, GameObject[]>();

    public Dictionary<DungeonThemes, GameObject> endRoomThemes = new Dictionary<DungeonThemes, GameObject>();

    private void Awake()
    {
        MainRegistry();
    }

    private void MainRegistry()
    {
        // Plague Stuff
        RegisterPlagueTypes();
        RegisterPlagueParts();

        // Ice Stuff
        RegisterIceTypes();
        RegisterIceParts();

        // Classic Stuff
        RegisterClassicTypes();
        RegisterClassicParts();

        RegisterEndRooms();
    }

    // Plague Registry

    private void RegisterPlagueParts()
    {
        themeTypes.Add(DungeonThemes.PLAGUE, plagueParts);
    }

    private void RegisterPlagueTypes()
    {
        plagueParts.Add(GeneratorType.LAYOUT, plagueLayouts);
    }

    // Ice Registry

    private void RegisterIceParts()
    {
        themeTypes.Add(DungeonThemes.ICE, iceParts);
    }

    private void RegisterIceTypes()
    {
        iceParts.Add(GeneratorType.LAYOUT, iceLayouts);
    }

    // Classic Registry

    private void RegisterClassicParts()
    {
        themeTypes.Add(DungeonThemes.CLASSIC, classicParts);
    }

    private void RegisterClassicTypes()
    {
        classicParts.Add(GeneratorType.LAYOUT, classicLayouts);
    }

    // End Registry

    private void RegisterEndRooms()
    {
        endRoomThemes.Add(DungeonThemes.DEBUG, endRooms[0]);
        endRoomThemes.Add(DungeonThemes.PLAGUE, endRooms[1]);
        endRoomThemes.Add(DungeonThemes.ICE, endRooms[2]);
        endRoomThemes.Add(DungeonThemes.CLASSIC, endRooms[3]);
    }
}