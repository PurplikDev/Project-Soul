using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLayoutSpawn : MonoBehaviour
{
    public GameObject layout;
    void Awake()
    {
        Instantiate(layout, transform.position, Quaternion.identity);
    }

}
