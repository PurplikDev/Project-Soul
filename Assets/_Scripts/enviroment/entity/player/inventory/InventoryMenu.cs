using UnityEngine.UIElements;

namespace roguelike.enviroment.entity.player.inventory {
    public class InventoryMenu {

        private Inventory _inventory;

        public InventoryMenu(Inventory inventory) {
            _inventory = inventory;
        }
    }
}