using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class Inventory : ItemContainer
    {
        [SerializeField] Item[] startingItems;
        [SerializeField] Transform itemsParent;

        protected override void Start()
        {
            base.Start();
            SetStartingItems();
            gameObject.SetActive(false);
        }

        protected override void OnValidate()
        {
            if(itemsParent != null)
            {
                itemsParent.GetComponentsInChildren(includeInactive: true, result: itemSlots);
            }
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

