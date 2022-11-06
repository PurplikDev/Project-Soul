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



    private void Start()
    {
        dungeonGenerationController = GameObject.Find("DungeonGenerationController");
        dungeonStorage = dungeonGenerationController.GetComponent<DungeonStorage>();
        dungeonGenerator = dungeonGenerationController.GetComponent<DungeonGenerator>();

        selectedType = dungeonStorage.themeTypes[dungeonStorage.selectedDungeonTheme];
        if(selectedGeneratorType == DungeonStorage.GeneratorType.ENDROOM) {
            if(!dungeonGenerator.hasExit)
            {
                Instantiate(dungeonStorage.endRoomThemes[dungeonStorage.selectedDungeonTheme], transform.position, Quaternion.identity);
                dungeonGenerator.hasExit = true;
            } else
            {
                Debug.Log("idk");
            }
        } else {
            selectedPart = selectedType[selectedGeneratorType];
            Instantiate(selectedPart[Random.Range(0, selectedPart.Length)], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
