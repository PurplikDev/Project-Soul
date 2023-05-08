using System;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class EquipmentInventory : MonoBehaviour
    {
        [SerializeField] Transform equipmentSlotsParent;
        [SerializeField] EquipmentSlot[] equipmentSlots;

        public event Action<Item> OnRightClickedEvent;

        private void Awake()
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].OnRightClickEvent += OnRightClickedEvent;
            }
        }
        private void OnValidate()
        {
            equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        }

        public bool AddItem(EquipmentItem item, out EquipmentItem previousItem)
        {
            for(int i = 0; i < equipmentSlots.Length; i++)
            {
                if(equipmentSlots[i].equipmentType == item.equipmentType)
                {
                    previousItem = (EquipmentItem)equipmentSlots[i].item;
                    equipmentSlots[i].item = item;
                    return true;
                }
            }
            previousItem = null;
            return false;
        }



        public bool RemoveItem(EquipmentItem item)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].item == item)
                {
                    equipmentSlots[i].item = null;
                    return true;
                }
            }
            return false;
        }
    }
}