using UnityEngine;
using io.purplik.ProjectSoul.Entity;
using UnityEngine.SceneManagement;

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
            if(dungeonController.IsItTimeToEnd())
            {
                dungeonController.Increase();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                dungeonController.loops = 0;
                SceneManager.LoadScene(1);
            }
        }
    }
}
