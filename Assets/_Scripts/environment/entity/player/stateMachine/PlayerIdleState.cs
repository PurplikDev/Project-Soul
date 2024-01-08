using UnityEngine;
using static roguelike.environment.entity.player.statemachine.PlayerStateMachine;

namespace roguelike.environment.entity.player.statemachine {
    public class PlayerIdleState : PlayerBaseState {        

        public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.IDLE) { }

        public override void EnterState() {
            playerStateMachine.Animator.SetTrigger("IdleTrigger");
        }

        public override void ExitState() { }

        public override void UpdateState() {
            if(!playerStateMachine.CharacterController.isGrounded) {
                playerStateMachine.CharacterController.SimpleMove(new Vector3());
            }
        }
    }
}
