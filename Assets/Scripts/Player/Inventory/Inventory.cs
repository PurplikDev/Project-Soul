using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Item> inventory = new List<Item>();
    private Dictionary<ItemStack, Item> itemDictionary = new Dictionary<ItemStack, Item>();

    private void OnEnable() {
        GoFastJuice.OnItemPickUp += PickUp;
    }

    private void OnDisable() {
        GoFastJuice.OnItemPickUp -= PickUp;
    }

    public void PickUp(ItemStack itemStack) {
        Item newItem = new Item(itemStack);
        inventory.Add(newItem);
    }

    public void Drop(ItemStack itemStack) {
        if(itemDictionary.TryGetValue(itemStack, out Item item)) {
            inventory.Remove(item);
            itemDictionary.Remove(itemStack);
        }
    }
}
