using UnityEngine;
using static roguelike.environment.ui.statemachine.UIStateMachine;

namespace roguelike.environment.ui.statemachine { 
    public class UIPauseState: UIBaseState {
        private GameObject _pauseUIHolder;

        public UIPauseState(UIStateMachine uiStateMachine, GameObject pauseUIHolder) : base(uiStateMachine) {
            _pauseUIHolder = pauseUIHolder;
        }

        public override void EnterState() { 
            _pauseUIHolder.SetActive(true);
        }

        public override void ExitState() {
            _pauseUIHolder.SetActive(false);
        }

        public override UIStates GetNextState() {
            return UIStates.NONE;
        }

        public override void UpdateState() { }
    }
}
