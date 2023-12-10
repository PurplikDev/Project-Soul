using roguelike.core.item;
using roguelike.enviroment.ui.statemachine;
using roguelike.system.input;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {

        public UIStateMachine UIStateMachine { get; set; }
        public PlayerInteractor PlayerInteractor { get; set; }
        public PlayerInput PlayerInput { get; private set; }
        public Inventory Inventory { get; private set; }

        protected override void Awake() {
            // input needs to be created first!!!!!
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();

            Inventory = new Inventory(this);
            PlayerInteractor = new PlayerInteractor(this);
            UIStateMachine = GetComponent<UIStateMachine>();

            base.Awake();
        }

        protected override void Update() {
            PlayerInteractor.UpdateInteractor();
        }
    }
}
