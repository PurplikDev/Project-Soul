using io.purplik.ProjectSoul.Entity;
using io.purplik.ProjectSoul.InventorySystem;
using io.purplik.ProjectSoul.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public int index;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerEntity>();
        if (player != null)
        {
            GameObject.Find("ItemSaveManager").GetComponent<ItemSaveManager>().SaveInventory(player.GetComponentInChildren<PlayerInventoryManager>());
            GameObject.Find("ItemSaveManager").GetComponent<ItemSaveManager>().SaveEquipment(player.GetComponentInChildren<PlayerInventoryManager>());
            SceneManager.LoadScene(index);
        }
    }
}
