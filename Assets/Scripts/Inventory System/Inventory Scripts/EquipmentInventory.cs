using System;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class EquipmentInventory : MonoBehaviour
    {
        [SerializeField] Transform equipmentSlotsParent;
        [SerializeField] EquipmentSlot[] equipmentSlots;

        public event Action<ItemSlot> OnRightClickEvent;

        public event Action<ItemSlot> OnPointerEnterEvent;
        public event Action<ItemSlot> OnPointerExitEvent;

        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;

        public event Action<ItemSlot> OnDragEvent;

        public event Action<ItemSlot> OnDropEvent;

        private void Start()
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
                equipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
                equipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
                equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
                equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
                equipmentSlots[i].OnDragEvent += OnDragEvent;
                equipmentSlots[i].OnDropEvent += OnDropEvent;
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