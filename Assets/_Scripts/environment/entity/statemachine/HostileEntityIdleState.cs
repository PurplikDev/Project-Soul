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
            if (stateMachine.isTargetting) {
                return EntityStates.CHASE;
            } else if (stateMachine.DoesWander) {
                return EntityStates.SEARCH;
            } else {
                return EntityStates.IDLE;
            }
        }
    }
}