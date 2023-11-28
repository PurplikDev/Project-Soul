using roguelike.core.item;
using roguelike.enviroment.entity.player.statemachine;
using roguelike.enviroment.ui.statemachine;
using roguelike.system.input;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {

        public UIStateMachine UIStateMachine { get; set; }
        public PlayerInteractor PlayerInteractor { get; set; }
        public PlayerInput PlayerInput { get; private set; }
        public Inventory Inventory { get; private set; }

        protected override void Awake() {
            UIStateMachine = GetComponent<UIStateMachine>();
            PlayerInteractor = new PlayerInteractor();
            PlayerInput = new PlayerInput();
            Inventory = new Inventory(this);

            PlayerInput.EnviromentControls.MouseMove.performed += PlayerInteractor.UpdateInteractor;

            base.Awake();
        }

        protected override void Update() {
        }
    }
}
