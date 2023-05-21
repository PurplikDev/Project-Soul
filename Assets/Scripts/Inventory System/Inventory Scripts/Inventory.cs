using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class Inventory : ItemContainer
    {
        [SerializeField] Transform itemsParent;

        protected override void OnValidate()
        {
            if (itemsParent != null)
                itemsParent.GetComponentsInChildren(includeInactive: true, result: itemSlots);

        }
    }
}

