using roguelike.environment.world.deployable;
using roguelike.rendering.ui;
using roguelike.system.manager;

namespace roguelike.environment.ui.statemachine { 
    public class UIDeployableState: UIBaseState {

        private Deployable deployable;
        private DeployableRenderer renderer;

        public UIDeployableState(UIStateMachine uiStateMachine, Deployable deployable) : base(uiStateMachine) {
            this.deployable = deployable;
        }

        public override void EnterState() {
            deployable.StationUIHolder.SetActive(true);
            renderer = deployable.GetRenderer(GameManager.Player);
        }

        public override void ExitState() {
            deployable.StationUIHolder.SetActive(false);
            renderer = null;
            stateMachine.OnDeployableExit();
        }

        public override void UpdateState() { }
    }
}
