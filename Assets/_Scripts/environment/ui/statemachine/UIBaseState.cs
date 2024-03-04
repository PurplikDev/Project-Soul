using roguelike.core.statemachine;
using UnityEngine;
using static roguelike.environment.ui.statemachine.UIStateMachine;

namespace roguelike.environment.ui.statemachine {
    public abstract class UIBaseState : BaseState<UIStates> {

        protected UIStateMachine stateMachine;

        public UIBaseState(UIStateMachine uiStateMachine) : base(UIStates.NONE) {
            stateMachine = uiStateMachine;
        }
        public override UIStates GetNextState() { return UIStates.NONE; }
    }
}
