using UnityEngine;
using static roguelike.enviroment.ui.statemachine.UIStateMachine;

namespace roguelike.enviroment.ui.statemachine { 
    public class UINoneState: UIBaseState {
        public UINoneState(UIStateMachine uiStateMachine) : base(uiStateMachine) { }

        public override void EnterState() {
            stateMachine.input.CharacterControls.Enable();
            Cursor.visible = false;
        }

        public override void ExitState() {
            stateMachine.input.CharacterControls.Disable();
            Cursor.visible = true;
        }

        public override UIStates GetNextState() {
            return UIStates.NONE;
        }

        public override void OnTriggerEnter(Collider collider) {
        }

        public override void OnTriggerExit(Collider collider) {
        }

        public override void OnTriggerStay(Collider collider) {
            
        }

        public override void UpdateState() { }
    }
}
