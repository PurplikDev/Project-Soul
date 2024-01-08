using roguelike.core.statemachine;
using UnityEngine;
using static roguelike.environment.entity.player.statemachine.PlayerStateMachine;

namespace roguelike.environment.entity.player.statemachine {
    public abstract class PlayerBaseState : BaseState<PlayerStates> {        

        protected PlayerStateMachine playerStateMachine;

        public PlayerBaseState(PlayerStateMachine stateMachine, PlayerStates state) : base(state) { 
            playerStateMachine = stateMachine;
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

        public override void OnTriggerEnter(Collider collider) { }

        public override void OnTriggerExit(Collider collider) { }

        public override void OnTriggerStay(Collider collider) { }
    }
}
