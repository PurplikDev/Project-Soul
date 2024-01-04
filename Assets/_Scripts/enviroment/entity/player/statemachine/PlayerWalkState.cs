using static roguelike.enviroment.entity.player.statemachine.PlayerStateMachine;

namespace roguelike.enviroment.entity.player.statemachine {
    public class PlayerWalkState : PlayerBaseState {
        public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine, PlayerStates.WALK) { }

        public override void EnterState() {
            playerStateMachine.Animator.SetTrigger("WalkTrigger");
        }

        public override void UpdateState() {
            playerStateMachine.CharacterController.SimpleMove(playerStateMachine.GetCurrentMovementSpeed);
        }

        public override void ExitState() { }
    }
}
