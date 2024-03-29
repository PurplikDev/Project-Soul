using roguelike.core.statemachine;
using UnityEngine;

namespace roguelike.environment.entity.player.statemachine {
    public abstract class PlayerBaseState : BaseState<PlayerStates> {        

        protected PlayerStateMachine playerStateMachine;

        public PlayerBaseState(PlayerStateMachine stateMachine, PlayerStates state) : base(state) { 
            playerStateMachine = stateMachine;
        }

        public override void ExitState() {
            playerStateMachine.facingRight = false;
            playerStateMachine.animator.ResetTrigger("WalkTrigger");
        }

        public override PlayerStates GetNextState() {
            if (playerStateMachine.IsMoving && !playerStateMachine.IsSprinting) {
                return PlayerStates.WALK;
            } else if (playerStateMachine.IsMoving && playerStateMachine.IsSprinting) {
                return PlayerStates.RUN;
            } else {
                return PlayerStates.IDLE;
            }
        }
    }
}
