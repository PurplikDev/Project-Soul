using roguelike.core.item;
using roguelike.enviroment.entity.player.StateMachine;
using roguelike.system.input;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {
        public InputReader inputReader;
        public Inventory PlayerInventory;
        public PlayerStateMachine PlayerStateMachine;

        private void Awake() {
            PlayerInventory = new Inventory(this);
            PlayerStateMachine = new PlayerStateMachine(this, inputReader);
        }

        protected override void Update() {
            PlayerStateMachine.UpdateStateMachine();
        }
    }
}
