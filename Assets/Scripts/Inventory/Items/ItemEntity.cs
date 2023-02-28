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

    void Awake()
    {
        model = Instantiate(item.itemModel, transform.position, transform.rotation, transform);
    }

    void Update()
    {
        model.transform.rotation = Quaternion.Euler(0, itemRotationY += Time.deltaTime * 50, 0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player")) { 
            collider.GetComponent<PlayerInventory>().inventory.AddItem(new Item(item), itemAmount);
            Destroy(gameObject);
        }
    }
}
