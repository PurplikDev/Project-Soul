namespace roguelike.enviroment.entity.player.StateMachine {
    public class PlayerAttackState : PlayerBaseState {
        public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }

        public override void EnterState() {

        }

        public override void UpdateState() {
            CheckSwitchStates();
        }

        public override void ExitState() {

        }

        public override void InitializeSubState() {

        }

        public override void CheckSwitchStates() {

        }
    }
}