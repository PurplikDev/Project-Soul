using UnityEngine;
using io.purplik.ProjectSoul.Entity;
using UnityEngine.SceneManagement;
using io.purplik.ProjectSoul.SaveSystem;
using io.purplik.ProjectSoul.InventorySystem;

public class DungeonEndRoom : MonoBehaviour
{
    public DungeonController dungeonController;

    private void Awake()
    {
        dungeonController = GameObject.Find("DungeonController").GetComponent<DungeonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerEntity>();
        if(player != null)
        {

            GameObject.Find("ItemSaveManager").GetComponent<ItemSaveManager>().SaveInventory(player.GetComponentInChildren<PlayerInventoryManager>());
            GameObject.Find("ItemSaveManager").GetComponent<ItemSaveManager>().SaveEquipment(player.GetComponentInChildren<PlayerInventoryManager>());

            if (dungeonController.IsItTimeToEnd())
            {
                dungeonController.Increase();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                dungeonController.loops = 0;
                SceneManager.LoadScene(3);
            }
        }
    }
}
