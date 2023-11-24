using roguelike.core.statemachine;
using UnityEngine;
using static roguelike.enviroment.entity.player.statemachine.PlayerStateMachine;

namespace roguelike.enviroment.entity.player.statemachine {
    public class PlayerIdleState : PlayerBaseState {        

        public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.IDLE) { }

        public override void EnterState() {
        }

        public override void ExitState() {
        }

        public override void OnTriggerEnter(Collider collider) {
        }

        public override void OnTriggerExit(Collider collider) {
        }

        public override void OnTriggerStay(Collider collider) {
            
        }

        public override void UpdateState() {
            if(!playerStateMachine.CharacterController.isGrounded) {
                playerStateMachine.CharacterController.SimpleMove(new Vector3());
            }
        }
    }
}