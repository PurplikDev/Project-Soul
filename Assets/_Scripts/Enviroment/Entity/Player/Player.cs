using roguelike.core.item;
using roguelike.enviroment.entity.player.StateMachine;
using roguelike.enviroment.world.deployable.workstation;
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

        protected override void Awake() {
            InputReader = Resources.LoadAll<InputReader>("data/player").First();
            PlayerInventory = new Inventory(this);
            PlayerStateMachine = new PlayerStateMachine(this, InputReader);

            inventoryRenderer = new InventoryRenderer(PlayerInventory, GetComponentInChildren<UIDocument>());

            GetComponentInChildren<UIDocument>().enabled = false;

            GameObject.Find("TestStation").GetComponent<CraftingStation>().OpenUI(this);

            base.Awake();
        }

        protected override void Update() {
            PlayerStateMachine.UpdateStateMachine();
        }
    }
}
