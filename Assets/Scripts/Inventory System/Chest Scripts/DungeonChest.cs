using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class DungeonChest : ItemChest
    {
        [SerializeField] ItemLootTable lootTable;

        protected override void Awake()
        {
            base.Awake();
            
            for(int i = 0; i < itemSlots.Count; i++)
            {
                itemSlots[i].item = GetRandomItem();
            }
        }

        private Item GetRandomItem()
        {
            float number = Random.Range(0f, 1f);
            if(number > 0.75f)
            {
                number = Random.Range(0f, lootTable.itemTable.Length);
                return lootTable.itemTable[(int) number];
            }

            return null;
        }
    }
}