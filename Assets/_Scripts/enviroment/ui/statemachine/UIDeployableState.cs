using roguelike.enviroment.world.deployable;
using roguelike.rendering.ui;
using roguelike.system.manager;
using UnityEngine;

namespace roguelike.enviroment.ui.statemachine { 
    public class UIDeployableState: UIBaseState {

        private Deployable deployable;
        private DeployableRenderer renderer;

        public UIDeployableState(UIStateMachine uiStateMachine) : base(uiStateMachine) { }

        public override void EnterState() {

            // todo: replace with logic that takes in a deployable that is being currently interacted with

            deployable = GameObject.Find("StorageCrate").GetComponent<Deployable>();
            deployable.StationUIHolder.SetActive(true);
            renderer = deployable.GetRenderer(GameManager.Instance.Player);
        }

        public override void ExitState() {
            deployable.StationUIHolder.SetActive(false);
            renderer = null;
        }

        public override void UpdateState() { }
    }
}
