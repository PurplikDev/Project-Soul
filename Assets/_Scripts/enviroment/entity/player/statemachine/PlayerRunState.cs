using roguelike.core.statemachine;
using UnityEngine;
using static roguelike.enviroment.entity.player.statemachine.PlayerStateMachine;

namespace roguelike.enviroment.entity.player.statemachine {
    public class PlayerRunState : PlayerBaseState {
        public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.RUN) { }

        public override void EnterState() {
        }

        public override void UpdateState() {
            playerStateMachine.CharacterController.SimpleMove(playerStateMachine.GetCurrentSprintSpeed);
        }

        public override void ExitState() {
        }

        public override void OnTriggerEnter(Collider collider) {
        }

        public override void OnTriggerExit(Collider collider) {
        }

        public override void OnTriggerStay(Collider collider) {
            
        }
    }
}
