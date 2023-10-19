using roguelike.enviroment.item;
using roguelike.enviroment.item.renderer;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using roguelike.core.utils;
using System.Linq;

namespace roguelike.enviroment.entity.player.inventory {
    [Serializable]
    public class Inventory {
        public List<ItemStack> InventoryItems = Enumerable.Repeat(ItemStack.EMPTY, 25).ToList();
        public List<ItemStack> EquipmentItems = Enumerable.Repeat(ItemStack.EMPTY, 6).ToList();
        public List<ItemStack> TrinketItems = Enumerable.Repeat(ItemStack.EMPTY, 8).ToList();

        private InventoryController _inventoryController;

        public Inventory(UIDocument document) {
            _inventoryController = new InventoryController(document, this);
        }
    }
}
