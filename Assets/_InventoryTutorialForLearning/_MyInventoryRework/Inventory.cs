using roguelike.enviroment.entity.player;
using System.Collections.Generic;

namespace roguelike.core.item {
    public class Inventory {

        public static readonly int InventorySize = 20;
        public List<ItemStack> Items = new List<ItemStack>(20);

        private Player _player;

        public Inventory(Player player) {
            _player = player;
            for(int i = 0; i < 20; i++) {
                if(i % 2 != 0) {
                    Items.Add(ItemStack.EMPTY);
                } else {
                    if(i % 4 == 0) {
                        Items.Add(new ItemStack(ItemManager.GetItemByID("test2"), i));
                    } else {
                        if(i % 3 == 0) {
                            Items.Add(new ItemStack(ItemManager.GetItemByID("test"), i));
                        } else {
                            Items.Add(new ItemStack(ItemManager.GetItemByID("test4"), i));
                        }
                    }
                }
            }
        }
    }
}
