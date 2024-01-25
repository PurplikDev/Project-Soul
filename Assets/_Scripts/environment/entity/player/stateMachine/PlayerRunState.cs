namespace roguelike.environment.entity.player.statemachine {
    public class PlayerRunState : PlayerBaseState {
        public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.RUN) { }

        public override void EnterState() {
        }

        public override void UpdateState() {
            playerStateMachine.CharacterController.SimpleMove(playerStateMachine.GetCurrentSprintSpeed);
        }
    }
}
