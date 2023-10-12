using roguelike.core.utils;
using roguelike.enviroment.item;
using System;

namespace roguelike.enviroment.entity.player.inventory {
    [Serializable]
    public class Inventory {
        public NonNullList<ItemStack> Items = new NonNullList<ItemStack>(24, ItemStack.EMPTY);
        public NonNullList<ItemStack> Equipment = new NonNullList<ItemStack>(6, ItemStack.EMPTY);
        public NonNullList<ItemStack> Trinkets = new NonNullList<ItemStack>(32, ItemStack.EMPTY);
    }
}
