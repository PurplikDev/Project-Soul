using io.purplik.ProjectSoul.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerEntity>();
        if(player != null)
        {
            SceneManager.LoadScene(2);
        }
    }
}
