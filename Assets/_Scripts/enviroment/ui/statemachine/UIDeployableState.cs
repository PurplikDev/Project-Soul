using UnityEngine;

namespace roguelike.enviroment.ui.statemachine { 
    public class UIDeployableState: UIBaseState {

        private GameObject _deployable;

        public UIDeployableState(UIStateMachine uiStateMachine) : base(uiStateMachine) { }

        public override void EnterState() {
            //_deployable.GetComponent
            //_deployable.SetActive(true);
        }

        public override void ExitState() {
            _deployable.SetActive(false);
        }

        public override UIStateMachine.UIStates GetNextState() {
            throw new System.NotImplementedException();
        }

        public override void UpdateState() {
            Debug.Log("inventory");
        }
    }
}
