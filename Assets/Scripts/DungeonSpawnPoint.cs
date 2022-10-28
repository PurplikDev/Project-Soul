using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawnPoint : MonoBehaviour
{

    public enum GeneratorType
    {
        LAYOUT,
        ROOM,
        CORRIDOR,
        ENDROOM
    }

    [SerializeField]
    GeneratorType selectedGeneratorType = new GeneratorType();

    private GameObject dungeonGenerationController;
    private DungeonStorage dungeonStorage;
    private DungeonGenerator dungeonGenerator;

    public Dictionary<DungeonStorage.DungeonThemes, Dictionary<GeneratorType, GameObject[]>> themeTypes = new Dictionary<DungeonStorage.DungeonThemes, Dictionary<GeneratorType, GameObject[]>>();

    public Dictionary<GeneratorType, GameObject[]> plagueParts = new Dictionary<GeneratorType, GameObject[]>();
    public Dictionary<GeneratorType, GameObject[]> iceParts = new Dictionary<GeneratorType, GameObject[]>();
    public Dictionary<GeneratorType, GameObject[]> classicParts = new Dictionary<GeneratorType, GameObject[]>();

    public Dictionary<DungeonStorage.DungeonThemes, GameObject> endRooms = new Dictionary<DungeonStorage.DungeonThemes, GameObject>();

    private Dictionary<GeneratorType, GameObject[]> selectedType;
    private GameObject[] selectedPart;



    private void Awake()
    {
        dungeonGenerationController = GameObject.Find("DungeonGenerationController");
        dungeonStorage = dungeonGenerationController.GetComponent<DungeonStorage>();
        dungeonGenerator = dungeonGenerationController.GetComponent<DungeonGenerator>();

        MainRegistry();

        selectedType = themeTypes[dungeonStorage.selectedDungeonTheme];
        selectedPart = selectedType[selectedGeneratorType];
        if(selectedGeneratorType == GeneratorType.ENDROOM && !dungeonGenerator.hasExit) {
            Instantiate(endRooms[dungeonStorage.selectedDungeonTheme], transform.position, Quaternion.identity);
            dungeonGenerator.hasExit = true;
        } else {
            Instantiate(selectedPart[Random.Range(0, selectedPart.Length)], transform.position, Quaternion.identity);
        }
        
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
        themeTypes.Add(DungeonStorage.DungeonThemes.PLAGUE, plagueParts);
    }

    private void RegisterPlagueTypes()
    {
        plagueParts.Add(GeneratorType.LAYOUT, dungeonStorage.plagueLayouts);
    }

    // Ice Registry

    private void RegisterIceParts()
    {
        themeTypes.Add(DungeonStorage.DungeonThemes.ICE, iceParts);
    }

    private void RegisterIceTypes()
    {
        iceParts.Add(GeneratorType.LAYOUT, dungeonStorage.iceLayouts);
    }

    // Classic Registry

    private void RegisterClassicParts()
    {
        themeTypes.Add(DungeonStorage.DungeonThemes.CLASSIC, classicParts);
    }

    private void RegisterClassicTypes()
    {
        classicParts.Add(GeneratorType.LAYOUT, dungeonStorage.classicLayouts);
    }

    // End Registry

    private void RegisterEndRooms()
    {
        endRooms.Add(DungeonStorage.DungeonThemes.DEBUG, dungeonStorage.endRooms[0]);
        endRooms.Add(DungeonStorage.DungeonThemes.PLAGUE, dungeonStorage.endRooms[1]);
        endRooms.Add(DungeonStorage.DungeonThemes.ICE, dungeonStorage.endRooms[2]);
        endRooms.Add(DungeonStorage.DungeonThemes.CLASSIC, dungeonStorage.endRooms[3]);
    }
}
