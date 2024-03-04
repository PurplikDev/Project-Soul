using roguelike.core.statemachine;
using UnityEngine;

namespace roguelike.environment.entity.statemachine {
    public abstract class HostileEntityBaseState : BaseState<EntityStates> {

        protected HostileEntityStateMachine stateMachine;

        public HostileEntityBaseState(HostileEntityStateMachine stateMachine, EntityStates key) : base(key) {
            this.stateMachine = stateMachine;
        }
    }
}