using roguelike.enviroment.entity;
using roguelike.enviroment.entity.player;
using roguelike.enviroment.entity.StatSystem;
using System.Collections.Generic;
using UnityEngine;

namespace roguelike.core.item {
    public class Inventory {

        public static readonly int InventorySize = 20; // this reffers to the size of the generic item storage, not the amount of all the slots
        public static readonly int EquipmentSize = 6;
        public static readonly int TrinketSize = 8;

        public List<ItemStack> Items = new List<ItemStack>(34);

        public Entity Entity;

        public Inventory(Entity entity) {
            Entity = entity;

            FillAllSlots();
        }

        private void FillAllSlots() {
            for (int i = 0; i < InventorySize; i++) {
                if (i % 2 != 0) {
                    if(i == 1) {
                        Items.Add(new ItemStack(ItemManager.GetItemByID("test_equipment")));
                    } else {
                        Items.Add(ItemStack.EMPTY);
                    }
                } else {
                    if (i % 4 == 0) {
                        Items.Add(new ItemStack(ItemManager.GetItemByID("test2"), i));
                    } else {
                        if (i % 3 == 0) {
                            Items.Add(new ItemStack(ItemManager.GetItemByID("test"), i));
                        } else {
                            Items.Add(new ItemStack(ItemManager.GetItemByID("test4"), i));
                        }
                    }
                }
            }

            for (int i = 0; i < EquipmentSize; i++) {
                Items.Add(ItemStack.EMPTY);
            }

            for (int i = 0; i < TrinketSize; i++) {
                Items.Add(ItemStack.EMPTY);
            }
        }

        public void UpdateItemStack(ItemStack itemStack, int index) {
            ItemStack oldStack = Items[index];
            Items[index] = itemStack;

            // The purpose of this check is to update stats only when necesseary.
            // There is no reason to update stats when an item in placed in a storage slot
            if(index >= InventorySize) {
                if(itemStack.Item is EquipmentItem item) {
                    foreach(StatModifier statModifier in item.StatModifiers) {
                        Entity.StatByType[statModifier.StatType].AddModifier(statModifier);
                    }
                } else if(itemStack.IsEmpty()) {
                    foreach(StatModifier statModifier in ((EquipmentItem)oldStack.Item).StatModifiers) {
                        Entity.StatByType[statModifier.StatType].RemoveModifier(statModifier);
                    }
                }
            }
        }
    }
}
