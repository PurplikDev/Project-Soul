using UnityEngine;
using static roguelike.enviroment.ui.statemachine.UIStateMachine;

namespace roguelike.enviroment.ui.statemachine { 
    public class UIPauseState: UIBaseState {
        public UIPauseState(UIStateMachine uiStateMachine) : base(uiStateMachine) { }

        public override void EnterState() { }

        public override void ExitState() { }

        public override UIStates GetNextState() {
            return UIStates.NONE;
        }

        public override void UpdateState() {
            Debug.Log("pause");
        }
    }
}
