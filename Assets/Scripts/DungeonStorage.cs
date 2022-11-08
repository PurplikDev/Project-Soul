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

    public enum GeneratorType
    {
        LAYOUT,
        ROOM,
        CORRIDOR,
        DEADEND
    }

    [SerializeField]
    public DungeonThemes selectedDungeonTheme = new DungeonThemes();

    [Header("Starting Rooms")]
    public GameObject[] spawnRooms;

    [Header("Exit Rooms")]
    public GameObject[] exitRooms;


    [Header("Layouts")]
    public GameObject[] plagueLayouts;
    public GameObject[] iceLayouts;
    public GameObject[] classicLayouts;

    [Header("Rooms")]
    public GameObject[] plagueRooms;
    public GameObject[] iceRooms;
    public GameObject[] classicRooms;

    [Header("Dead Ends")]
    public GameObject[] plagueDeadEnds;
    public GameObject[] iceDeadEnds;
    public GameObject[] classicDeadEnds;

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
        RegisterPlague();

        // Ice Stuff
        RegisterIce();

        // Classic Stuff
        RegisterClassic();

        // Exit Rooms
        RegisterExitRooms();
    }

    // Plague Registry

    private void RegisterPlague()
    {
        // THEME
        themeTypes.Add(DungeonThemes.PLAGUE, plagueParts);

        // PARTS
        plagueParts.Add(GeneratorType.LAYOUT, plagueLayouts);
        plagueParts.Add(GeneratorType.DEADEND, plagueDeadEnds);

    }

    // Ice Registry

    private void RegisterIce()
    {
        // THEME
        themeTypes.Add(DungeonThemes.ICE, iceParts);

        // PARTS
        iceParts.Add(GeneratorType.LAYOUT, iceLayouts);
        iceParts.Add(GeneratorType.DEADEND, iceDeadEnds);
    }

    // Classic Registry

    private void RegisterClassic()
    {
        // THEME
        themeTypes.Add(DungeonThemes.CLASSIC, classicParts);

        // PARTS
        classicParts.Add(GeneratorType.LAYOUT, classicLayouts);
        classicParts.Add(GeneratorType.DEADEND, classicDeadEnds);
    }

    // End Registry

    private void RegisterExitRooms()
    {
        endRoomThemes.Add(DungeonThemes.DEBUG, exitRooms[0]);
        endRoomThemes.Add(DungeonThemes.PLAGUE, exitRooms[1]);
        endRoomThemes.Add(DungeonThemes.ICE, exitRooms[2]);
        endRoomThemes.Add(DungeonThemes.CLASSIC, exitRooms[3]);
    }
}