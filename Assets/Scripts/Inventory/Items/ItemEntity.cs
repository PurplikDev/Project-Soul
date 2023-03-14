using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour
{
    public ItemObject item;
    private GameObject model;

    [Range(1, 32)]
    public int itemAmount = 1;

    private float itemRotationY;
    private bool pickable = false;

    private void Awake()
    {
        Invoke("MakePickable", 2.5f);
    }

    void Start()
    {
        model = Instantiate(item.itemModel, transform.position, transform.rotation, transform);
    }

    void Update()
    {
        model.transform.rotation = Quaternion.Euler(0, itemRotationY += Time.deltaTime * 50, 0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!pickable) {
            return;
        }
        var playerInventory = collider.GetComponent<PlayerEntity>();
        if(playerInventory != null && playerInventory.inventory.CheckForEmptySlot()) {
            playerInventory.inventory.AddItem(new Item(item), itemAmount);
            Destroy(gameObject);
        }
    }

    private void MakePickable() {
        pickable = true;
    }

}
