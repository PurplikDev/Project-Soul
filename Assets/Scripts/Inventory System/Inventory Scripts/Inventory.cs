using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class Inventory : MonoBehaviour, IItemContainer
    {
        [SerializeField] List<Item> startingItems;
        [SerializeField] Transform itemsParent;
        [SerializeField] ItemSlot[] itemSlots;

        public event Action<ItemSlot> OnRightClickEvent;

        public event Action<ItemSlot> OnPointerEnterEvent;
        public event Action<ItemSlot> OnPointerExitEvent;

        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;

        public event Action<ItemSlot> OnDragEvent;

        public event Action<ItemSlot> OnDropEvent;

        private void Start()
        {
            for(int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnRightClickEvent += OnRightClickEvent;
                itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
                itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
                itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
                itemSlots[i].OnEndDragEvent += OnEndDragEvent;
                itemSlots[i].OnDragEvent += OnDragEvent;
                itemSlots[i].OnDropEvent += OnDropEvent;
            }

            SetStartingItems();
        }

        private void OnValidate()
        {
            if(itemsParent != null)
            {
                itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
            }

            SetStartingItems();
        }

        private void SetStartingItems()
        {
            int i = 0;
            for(; i < startingItems.Count && i < itemSlots.Length; i++)
            {
                itemSlots[i].item = startingItems[i].GetCopy();
                itemSlots[i].itemAmount = 1;
            }

            for(; i < itemSlots.Length; i++)
            {
                itemSlots[i].item = null;
                itemSlots[i].itemAmount = 0;
            }
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    itemSlots[i].item = item;
                    itemSlots[i].itemAmount++;
                    return true;
                }
            }
            return false;
        }



        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == item)
                {
                    itemSlots[i].itemAmount--;

                    return true;
                }
            }
            return false;
        }

        public bool IsFull()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    return false;
                }
            }
            return true;
        }

        public int ItemCount(string itemID)
        {
            int amount = 0;

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item.ID == itemID)
                {
                    amount++;
                }
            }

            return amount;
        }

        public Item RemoveItem(string itemID)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                Item item = itemSlots[i].item;
                if(item != null && item.ID == itemID)
                {
                    itemSlots[i].itemAmount--; ;
                    return item;
                }
            }
            return null;
        }
    }
}

