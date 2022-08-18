using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider) {
        IPickupable pickupable = collider.GetComponent<IPickupable>();
        if(pickupable != null) {
            pickupable.Pickup();
        }
    }
}
