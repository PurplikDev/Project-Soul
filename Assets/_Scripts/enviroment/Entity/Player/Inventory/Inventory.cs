using roguelike.enviroment.item;
using roguelike.enviroment.item.renderer;

using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace roguelike.enviroment.entity.player.inventory {
    [Serializable]
    public class Inventory {
        public List<ItemStack> InventoryItems = new List<ItemStack>(20);
    }
}
