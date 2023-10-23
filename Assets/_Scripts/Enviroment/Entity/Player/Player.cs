using roguelike.enviroment.entity.player.inventory;
using roguelike.enviroment.item.renderer;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {
        [Header("Inventory")]
        public Inventory PlayerInventory;
        public InventoryController PlayerInventoryController;

        private void Awake() {
            PlayerInventory = new Inventory();
            PlayerInventoryController = new InventoryController(PlayerInventory);
        }
    }
}
