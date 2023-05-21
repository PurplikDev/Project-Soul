using System;
using System.Collections.Generic;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class ItemContainer : MonoBehaviour, IItemContainer
    {
        public List<ItemSlot> itemSlots;

        public event Action<ItemSlot> OnRightClickEvent;

        public event Action<ItemSlot> OnPointerEnterEvent;
        public event Action<ItemSlot> OnPointerExitEvent;

        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;

        public event Action<ItemSlot> OnDragEvent;

        public event Action<ItemSlot> OnDropEvent;

        protected virtual void OnValidate()
        {
            GetComponentsInChildren(includeInactive: true, result: itemSlots);
        }

        protected virtual void Awake()
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                itemSlots[i].OnPointerEnterEvent += slot => EventHelper(slot, OnPointerEnterEvent);
                itemSlots[i].OnPointerExitEvent += slot => EventHelper(slot, OnPointerExitEvent);
                itemSlots[i].OnRightClickEvent += slot => EventHelper(slot, OnRightClickEvent);
                itemSlots[i].OnBeginDragEvent += slot => EventHelper(slot, OnBeginDragEvent);
                itemSlots[i].OnEndDragEvent += slot => EventHelper(slot, OnEndDragEvent);
                itemSlots[i].OnDragEvent += slot => EventHelper(slot, OnDragEvent);
                itemSlots[i].OnDropEvent += slot => EventHelper(slot, OnDropEvent);
            }
        }

        private void EventHelper(ItemSlot itemSlot, Action<ItemSlot> action)
        {
            if (action != null)
                action(itemSlot);
        }

        public virtual bool CanAddItem(Item item, int amount = 1)
        {
            int freeSpaces = 0;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item == null || itemSlot.item.ID == item.ID)
                {
                    freeSpaces += item.maxStackSize - itemSlot.itemAmount;
                }
            }
            return freeSpaces >= amount;
        }

        public virtual bool AddItem(Item item)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].CanAddStack(item))
                {
                    itemSlots[i].item = item;
                    itemSlots[i].itemAmount++;
                    return true;
                }
            }

            for (int i = 0; i < itemSlots.Count; i++)
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

        public virtual bool RemoveItem(Item item)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].item == item)
                {
                    itemSlots[i].itemAmount--;
                    return true;
                }
            }
            return false;
        }

        public virtual Item RemoveItem(string itemID)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                Item item = itemSlots[i].item;
                if (item != null && item.ID == itemID)
                {
                    itemSlots[i].itemAmount--;
                    return item;
                }
            }
            return null;
        }

        public virtual int ItemCount(string itemID)
        {
            int number = 0;

            for (int i = 0; i < itemSlots.Count; i++)
            {
                Item item = itemSlots[i].item;
                if (item != null && item.ID == itemID)
                {
                    number += itemSlots[i].itemAmount;
                }
            }
            return number;
        }

        public void Clear()
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].item != null && Application.isPlaying)
                {
                    itemSlots[i].item.Destory();
                }
                itemSlots[i].item = null;
                itemSlots[i].itemAmount = 0;
            }
        }
    }
}
