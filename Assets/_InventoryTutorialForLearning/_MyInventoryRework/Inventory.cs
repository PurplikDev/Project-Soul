using roguelike.enviroment.entity.player;
using System.Collections.Generic;

namespace roguelike.core.item {
    public class Inventory {

        public static readonly int InventorySize = 20;
        public List<ItemStack> Items = new List<ItemStack>(20);

        private Player _player;

        public Inventory(Player player) {
            _player = player;
        }
    }
}
