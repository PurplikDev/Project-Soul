using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public class HostileEntityIdleState : HostileEntityBaseState {
        public HostileEntityIdleState(HostileEntityStateMachine stateMachine) : base(stateMachine, EntityStates.IDLE) {
        }

        public override void EnterState() { }

        public override void UpdateState() {
            stateMachine.entityController.SimpleMove(new Vector3());
        }

        public override void ExitState() { }



        public override EntityStates GetNextState() {
            if(stateMachine.NeedToSeePlayer && stateMachine.canSeePlayer ||
                !stateMachine.NeedToSeePlayer && stateMachine.isPlayerInRange) {
                return EntityStates.CHASE;
            } else {
                return EntityStates.IDLE;
            }
        }
    }
}