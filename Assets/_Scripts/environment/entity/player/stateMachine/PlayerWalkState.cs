namespace roguelike.environment.entity.player.statemachine {
    public class PlayerWalkState : PlayerBaseState {
        public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.WALK) { }

        public override void EnterState() {
            playerStateMachine.animator.SetTrigger("WalkTrigger");
            playerStateMachine.player.MovementStartEvent.Invoke();
        }

        public override void ExitState() {
            playerStateMachine.player.MovementStopEvent.Invoke();
            base.ExitState();
        }

        public override void UpdateState() {
            playerStateMachine.CharacterController.SimpleMove(playerStateMachine.GetCurrentMovementSpeed);
        }
    }
}
