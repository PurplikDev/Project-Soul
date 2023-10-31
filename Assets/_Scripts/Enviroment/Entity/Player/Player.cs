using roguelike.core.item;
using roguelike.enviroment.entity.player.StateMachine;
using roguelike.rendering.ui;
using roguelike.system.input;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {

        public InputReader InputReader;
        public Inventory PlayerInventory;
        public PlayerStateMachine PlayerStateMachine;
        public InventoryRenderer inventoryRenderer;

        public UIDocument playerInventoryUI;

        private void Awake() {

            this.InputReader = Resources.LoadAll<InputReader>("data/player").First();
            PlayerInventory = new Inventory(this);
            PlayerStateMachine = new PlayerStateMachine(this, InputReader);

            inventoryRenderer = new InventoryRenderer(PlayerInventory, GetComponentInChildren<UIDocument>());
        }

        protected override void Update() {
            PlayerStateMachine.UpdateStateMachine();
        }
    }
}
