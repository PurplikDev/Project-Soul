using roguelike.core.statemachine;
using UnityEngine;
using static roguelike.enviroment.ui.statemachine.UIStateMachine;

namespace roguelike.enviroment.ui.statemachine {
    public abstract class UIBaseState : BaseState<UIStates> {

        protected UIStateMachine stateMachine;

        public UIBaseState(UIStateMachine uiStateMachine) : base(UIStates.NONE) {
            stateMachine = uiStateMachine;
        }
        public override UIStates GetNextState() { return UIStates.NONE; }
        public override void OnTriggerEnter(Collider collider) { }
        public override void OnTriggerExit(Collider collider) { }
        public override void OnTriggerStay(Collider collider) { }
    }
}
