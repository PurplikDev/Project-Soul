using roguelike.environment.entity.player.statemachine;
using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public class HostileEntityChaseState : HostileEntityBaseState {
        public HostileEntityChaseState(HostileEntityStateMachine stateMachine) : base(stateMachine, EntityStates.CHASE) { }

        public override void EnterState() {
            Debug.Log("chase state entered");
        }

        public override void UpdateState() {
            stateMachine.entityController.SimpleMove(stateMachine.GetCurrentMovementSpeed);
        }

        public override void ExitState() { }
    }
}