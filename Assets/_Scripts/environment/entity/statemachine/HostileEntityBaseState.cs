using roguelike.core.statemachine;
using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public abstract class HostileEntityBaseState : BaseState<EntityStates> {

        protected HostileEntityStateMachine stateMachine;

        public HostileEntityBaseState(HostileEntityStateMachine stateMachine, EntityStates key) : base(key) {
            this.stateMachine = stateMachine;
        }


        public override EntityStates GetNextState() {
            if (stateMachine.NeedToSeePlayer && stateMachine.canSeePlayer || 
                !stateMachine.NeedToSeePlayer && stateMachine.isPlayerInRange) {
                return EntityStates.CHASE;
            } else {
                return EntityStates.IDLE;
            }
        }

        public override void OnTriggerEnter(Collider collider) { }

        public override void OnTriggerStay(Collider collider) { }

        public override void OnTriggerExit(Collider collider) { }

        
    }
}