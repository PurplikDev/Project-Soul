using roguelike.core.utils;
using roguelike.enviroment.item;
using System;
using System.Collections.Generic;

namespace roguelike.enviroment.entity.player.inventory {
    [Serializable]
    public class Inventory {

        public NonNullList<ItemStack> Items = new NonNullList<ItemStack>(32, ItemStack.EMPTY);
        public List<ItemStack> Equipment = new List<ItemStack>(32);
        public List<ItemStack> Trinkets = new List<ItemStack>(32);

    }
}
