using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public class HostileEntitySearchState : HostileEntityBaseState {
        public HostileEntitySearchState(HostileEntityStateMachine stateMachine) : base(stateMachine, EntityStates.SEARCH) { }

        public override void EnterState() {
            Debug.Log("entered search");
        }

        public override void UpdateState() {

        }

        public override void ExitState() {
            Debug.Log("exited search");
        }

        public override EntityStates GetNextState() {
            return EntityStates.IDLE;
        }
    }
}