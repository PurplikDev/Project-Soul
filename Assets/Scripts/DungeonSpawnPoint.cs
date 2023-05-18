using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawnPoint : MonoBehaviour
{

    [SerializeField]
    DungeonStorage.GeneratorType selectedGeneratorType = new DungeonStorage.GeneratorType();

    private GameObject dungeonGenerationController;
    private DungeonStorage dungeonStorage;
    private DungeonGenerator dungeonGenerator;

    private Dictionary<DungeonStorage.GeneratorType, GameObject[]> selectedType;
    private GameObject[] selectedPart;

    private int randomChance;

    void Awake()
    {
        dungeonGenerationController = GameObject.Find("DungeonGenerationController");
        dungeonStorage = dungeonGenerationController.GetComponent<DungeonStorage>();
        dungeonGenerator = dungeonGenerationController.GetComponent<DungeonGenerator>();

        selectedType = dungeonStorage.themeTypes[dungeonStorage.selectedDungeonTheme];

        // GENERATES A DUNGEON PART IF IT IS NOT A DEAD END

        if(selectedGeneratorType != DungeonStorage.GeneratorType.DEADEND) {
            selectedPart = selectedType[selectedGeneratorType];
            Instantiate(selectedPart[Random.Range(0, selectedPart.Length)], transform.position, transform.rotation);
        } else { 
            dungeonGenerator.amountOfExits++;
        }
    }

    void Start()
    {
        // GENERATES AN EXIT OR A DEAD END

        if(selectedGeneratorType == DungeonStorage.GeneratorType.DEADEND)
        {
            dungeonGenerationController = GameObject.Find("DungeonGenerationController");
            dungeonStorage = dungeonGenerationController.GetComponent<DungeonStorage>();
            dungeonGenerator = dungeonGenerationController.GetComponent<DungeonGenerator>();
            selectedPart = selectedType[selectedGeneratorType];

            randomChance = Random.Range(0, 100) % 3;

            if (!dungeonGenerator.hasExit)
            {
                if (randomChance != 2) {
                    if (dungeonGenerator.amountOfExits == 1)
                    {
                        Instantiate(dungeonStorage.endRoomThemes[dungeonStorage.selectedDungeonTheme], transform.position, transform.rotation);
                        dungeonGenerator.hasExit = true;
                    }
                    else
                    {
                        Instantiate(selectedPart[Random.Range(0, selectedPart.Length)], transform.position, transform.rotation);
                    }
                } else
                {
                    Instantiate(dungeonStorage.endRoomThemes[dungeonStorage.selectedDungeonTheme], transform.position, transform.rotation);
                    dungeonGenerator.hasExit = true;
                }
            }
            else
            {
                Instantiate(selectedPart[Random.Range(0, selectedPart.Length)], transform.position, transform.rotation);
            }

            dungeonGenerator.amountOfExits--;
            Destroy(gameObject);
        }
    }
}
