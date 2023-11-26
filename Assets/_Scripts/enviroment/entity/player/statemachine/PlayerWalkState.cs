using roguelike.core.statemachine;
using UnityEngine;
using static roguelike.enviroment.entity.player.statemachine.PlayerStateMachine;

namespace roguelike.enviroment.entity.player.statemachine {
    public class PlayerWalkState : PlayerBaseState {
        public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.WALK) { }

        public override void EnterState() { }

        public override void UpdateState() {
            playerStateMachine.CharacterController.SimpleMove(playerStateMachine.GetCurrentMovementSpeed);
        }

        public override void ExitState() { }

        public override void OnTriggerEnter(Collider collider) {
        }

        public override void OnTriggerExit(Collider collider) {
        }

        public override void OnTriggerStay(Collider collider) {
            
        }
    }
}
