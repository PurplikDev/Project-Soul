using roguelike.core.statemachine;
using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public abstract class HostileEntityBaseState : BaseState<EntityStates> {

        protected HostileEntityStateMachine stateMachine;

        public HostileEntityBaseState(HostileEntityStateMachine stateMachine, EntityStates key) : base(key) {
            this.stateMachine = stateMachine;
        }

        public override void OnTriggerEnter(Collider collider) { }

        public override void OnTriggerStay(Collider collider) { }

        public override void OnTriggerExit(Collider collider) { }

        
    }
}