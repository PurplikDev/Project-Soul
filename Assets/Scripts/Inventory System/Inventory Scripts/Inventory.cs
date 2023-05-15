using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class Inventory : ItemContainer
    {
        [SerializeField] Item[] startingItems;
        [SerializeField] Transform itemsParent;

        protected override void Awake()
        {
            base.Awake();
            SetStartingItems();
        }

        protected override void OnValidate()
        {
            if(itemsParent != null)
            {
                itemsParent.GetComponentsInChildren(includeInactive: true, result: itemSlots);
            }

            SetStartingItems();
        }

        private void SetStartingItems()
        {
            for(int i = 0; i < startingItems.Length; i++)
            {
                AddItem(startingItems[i].GetCopy());
            }
        }
    }
}

