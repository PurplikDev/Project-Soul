using roguelike.core.item;
using roguelike.enviroment.entity.player.statemachine;
using roguelike.enviroment.ui.statemachine;
using roguelike.system.input;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {

        public UIStateMachine UIStateMachine { get; set; }

        public PlayerInput PlayerInput { get; private set; }
        public Inventory Inventory { get; private set; }

        protected override void Awake() {
            UIStateMachine = GetComponent<UIStateMachine>();

            PlayerInput = new PlayerInput();
            Inventory = new Inventory(this);
            base.Awake();
        }

        protected override void Update() {
        }
    }
}
