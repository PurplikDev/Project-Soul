using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject equipmentSlots;
    [SerializeField] GameObject trinketSlots;
    [SerializeField] GameObject invetoryUI;
    GameObject pouchSlot;

    PlayerEntity playerEntity;

    private void Awake()
    {
        playerEntity = GetComponent<PlayerEntity>();
        pouchSlot = trinketSlots.transform.GetChild(5).gameObject;
    }

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

    private void Update()
    {
        if(Input.GetKeyDown(playerEntity.keybindManager.openInventory)) {
            invetoryUI.SetActive(!invetoryUI.activeInHierarchy);
        }

        if (playerEntity.inventory.GetSlots[33].item.ID > 0)
        {
            pouchSlot.SetActive(true);
        } else
        {
            pouchSlot.SetActive(false);
        }
    }
}
