using roguelike.enviroment.entity.player.inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {
        [Header("Inventory")]
        public Inventory inventory;
        [SerializeField] private UIDocument inventoryDocument;

        private void Awake() {
            inventory = new Inventory(inventoryDocument);
        }
    }
}
