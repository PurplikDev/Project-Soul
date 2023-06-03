using io.purplik.ProjectSoul.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicChest : ItemChest
{
    [SerializeField] Transform mimic;
    public override void Interact()
    {
        Instantiate(mimic, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
