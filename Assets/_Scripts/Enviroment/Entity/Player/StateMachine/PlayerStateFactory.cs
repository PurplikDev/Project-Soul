namespace Roguelike.Enviroment.Entity.Player.StateMachine {
    public class PlayerStateFactory {
        PlayerStateMachine _context;

        public PlayerStateFactory(PlayerStateMachine currentContext) {
            _context = currentContext;
        }

        public PlayerBaseState Idle() {
            return new PlayerIdleState(_context, this);
        }
        public PlayerBaseState Movement() {
            return new PlayerMovementState(_context, this);
        }

        public PlayerBaseState Attack() {
            return new PlayerAttackState(_context, this);
        }
        public PlayerBaseState Walk() {
            return new PlayerWalkState(_context, this);
        }
    }
}