using System;
using UnityEngine;

public class GoFastJuice : MonoBehaviour, IPickupable
{
    public static event PickUpItem OnItemPickUp;
    public delegate void PickUpItem(ItemStack itemStack);
    public ItemStack itemStack;
    public void Pickup() {
        Debug.Log("speeeeeed");
        Destroy(gameObject);
        OnItemPickUp?.Invoke(itemStack);
    }
}
