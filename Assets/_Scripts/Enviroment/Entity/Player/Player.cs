using roguelike.core.item;
using roguelike.enviroment.entity.player.StateMachine;
using roguelike.rendering.ui;
using roguelike.system.input;
using roguelike.system.manager;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {

        public InputReader InputReader;
        public Inventory PlayerInventory;
        public PlayerStateMachine PlayerStateMachine;

        //private UIDocument _document;
        //private InventoryRenderer _inventoryRenderer;

        protected override void Awake() {
            InputReader = Resources.LoadAll<InputReader>("data/player").First();
            PlayerInventory = new Inventory(this);
            PlayerStateMachine = new PlayerStateMachine(this, InputReader);

            //_document = GameObject.Find("InventoryUIHolder").GetComponent<UIDocument>();
            //_inventoryRenderer = new InventoryRenderer(this.PlayerInventory, _document);

            base.Awake();
        }

        private void Start() {
        }

        protected override void Update() {
            PlayerStateMachine.UpdateStateMachine();
        }
    }
}
