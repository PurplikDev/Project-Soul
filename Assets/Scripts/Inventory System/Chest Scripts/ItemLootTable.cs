using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    [CreateAssetMenu (fileName = "New Loot Table", menuName = "Inventory System/Loot Table")]
    public class ItemLootTable : ScriptableObject
    {
        public Item[] itemTable;
    }
}