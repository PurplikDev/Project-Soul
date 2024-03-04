using UnityEngine;
using static roguelike.environment.ui.statemachine.UIStateMachine;

namespace roguelike.environment.ui.statemachine { 
    public class UINoneState: UIBaseState {
        public UINoneState(UIStateMachine uiStateMachine) : base(uiStateMachine) { }

        public override void EnterState() {
            stateMachine.input.CharacterControls.Enable();
            stateMachine.input.EnviromentControls.Enable();
        }

        public override void ExitState() {
            stateMachine.input.CharacterControls.Disable();
            stateMachine.input.EnviromentControls.Disable();
        }

        public override UIStates GetNextState() {
            return UIStates.NONE;
        }

        public override void UpdateState() { }
    }
}
