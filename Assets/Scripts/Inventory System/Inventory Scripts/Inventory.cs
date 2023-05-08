using System;
using System.Collections.Generic;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] List<Item> items;
        [SerializeField] Transform itemsParent;
        [SerializeField] ItemSlot[] itemSlots;

        public event Action<Item> OnRightClickedEvent;

        private void Awake()
        {
            for(int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnRightClickEvent += OnRightClickedEvent;
            }
        }

        private void OnValidate()
        {
            if(itemsParent != null)
            {
                itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            int i = 0;
            for(; i < items.Count && i < itemSlots.Length; i++)
            {
                itemSlots[i].item = items[i];
            }

            for(; i < itemSlots.Length; i++)
            {
                itemSlots[i].item = null;
            }
        }

        public bool AddItem(Item item)
        {
            if (IsFull())
            {
                return false;
            }
            items.Add(item);
            RefreshUI();
            return true;
        }

        public bool RemoveItem(Item item)
        {
            if(items.Remove(item))
            {
                RefreshUI();
                return true;
            }
            return false;
        }

        public bool IsFull()
        {
            return items.Count >= itemSlots.Length;
        }
    }
}

