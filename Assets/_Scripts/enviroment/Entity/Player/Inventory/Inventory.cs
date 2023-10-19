using roguelike.core.utils;
using roguelike.enviroment.item;
using System;
using System.Collections.Generic;

namespace roguelike.enviroment.entity.player.inventory {
    [Serializable]
    public class Inventory {
        public List<ItemStack> InventoryItems = new List<ItemStack>(24);
        public List<ItemStack> EquipmentItems = new List<ItemStack>(6);
        public List<ItemStack> TrinketItems = new List<ItemStack>(8);

    }
}
