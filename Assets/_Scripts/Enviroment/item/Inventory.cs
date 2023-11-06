using roguelike.enviroment.entity.player;
using System.Collections.Generic;
using UnityEngine;

namespace roguelike.core.item {
    public class Inventory {

        public static readonly int InventorySize = 20; // this reffers to the size of the generic item storage, not the amount of all the slots
        public static readonly int EquipmentSize = 6;
        public static readonly int TrinketSize = 8;

        public List<ItemStack> Items = new List<ItemStack>(34);

        private Player _player;

        public Inventory(Player player) {
            _player = player;

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
            Items[index] = itemStack;
            if(index >= InventorySize) {
                // recalculate stats
            }
        }
    }
}
