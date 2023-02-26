using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject equipmentSlots;
    [SerializeField] GameObject trinketSlots;

    public void DisplayEquipment()
    {
        equipmentSlots.SetActive(true);
        trinketSlots.SetActive(false);
    }

    public void DisplayTrinkets()
    {
        equipmentSlots.SetActive(false);
        trinketSlots.SetActive(true);
    }

}
