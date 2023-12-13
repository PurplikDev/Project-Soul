using roguelike.core.item;
using roguelike.enviroment.entity.statsystem;
using roguelike.enviroment.ui.statemachine;
using roguelike.system.input;
using static roguelike.core.item.Inventory;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {

        public Stat Corruption = new Stat(0); // tldr; corruption

        public UIStateMachine UIStateMachine { get; set; }
        public PlayerInteractor PlayerInteractor { get; set; }
        public PlayerInput PlayerInput { get; private set; }
        public Inventory Inventory { get; private set; }

        public WeaponItem ItemInMainHand { get {
                var item = Inventory.Items[(int)InventorySlot.MAIN_HAND].Item;
                if(item is WeaponItem weapon) { return weapon; }
                return null; 
            } }

        protected override void Awake() {
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

        public override void Attack() {
            ItemInMainHand?.Attack(this);
        }
    }
}
