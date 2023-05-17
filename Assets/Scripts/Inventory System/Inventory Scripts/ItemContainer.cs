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

        protected virtual void Start()
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                itemSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
                itemSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
                itemSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
                itemSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
                itemSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
                itemSlots[i].OnDragEvent += slot => OnDragEvent(slot);
                itemSlots[i].OnDropEvent += slot => OnDropEvent(slot);
            }
        }

        public bool AddItem(Item item)
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

        public virtual bool CanAddItem(Item item, int amount = 1)
        {
            int freeSpace = 0;

            foreach (ItemSlot slot in itemSlots)
            {
                if (slot.item == null || slot.item.ID == item.ID)
                {
                    freeSpace += item.maxStackSize - slot.itemAmount;
                }
            }

            return freeSpace >= amount;
        }

        public bool RemoveItem(Item item)
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

        public Item RemoveItem(string itemID)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                Item item = itemSlots[i].item;
                if (item != null && item.ID == itemID)
                {
                    itemSlots[i].itemAmount--; ;
                    return item;
                }
            }
            return null;
        }

        public int ItemCount(string itemID)
        {
            int amount = 0;

            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].item.ID == itemID)
                {
                    amount += itemSlots[i].itemAmount;
                }
            }

            return amount;
        }

        public virtual void Clear()
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
