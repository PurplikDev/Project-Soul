using System.Collections.Generic;
using UnityEngine;

namespace roguelike.core.item.loottable {
    [CreateAssetMenu(fileName = "New Loot Table", menuName = "Items/LootTables")]
    public class LootTable : ScriptableObject {
        public List<string> LootTableItems = new List<string>();

        public ItemStack GetRandomLoot() {
            int random = Random.Range(0, LootTableItems.Count);

            ItemStack lootItem = new ItemStack(ItemManager.GetItemByID(LootTableItems[random]));
            lootItem.SetStackSize(Random.Range(1, lootItem.Item.MaxStackSize / 2));

            return lootItem;
        }
    }
}