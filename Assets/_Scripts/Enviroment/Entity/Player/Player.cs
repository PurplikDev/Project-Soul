using roguelike.core.item;
using roguelike.enviroment.entity.player.StateMachine;
using roguelike.system.input;
using System.Linq;
using UnityEngine;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {
        public InputReader InputReader;
        public Inventory PlayerInventory;
        public PlayerStateMachine PlayerStateMachine;

        private void Awake() {
            this.InputReader = Resources.LoadAll<InputReader>("data/player").First();
            PlayerInventory = new Inventory(this);
            PlayerStateMachine = new PlayerStateMachine(this, InputReader);
        }

        protected override void Update() {
            PlayerStateMachine.UpdateStateMachine();
        }
    }
}
