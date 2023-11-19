using roguelike.core.item;
using roguelike.system.input;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {

        public PlayerInput PlayerInput { get; private set; }
        public Inventory Inventory { get; private set; }

        protected override void Awake() {
            PlayerInput = new PlayerInput();
            Inventory = new Inventory(this);
            base.Awake();
        }

        protected override void Update() {
        }
    }
}
