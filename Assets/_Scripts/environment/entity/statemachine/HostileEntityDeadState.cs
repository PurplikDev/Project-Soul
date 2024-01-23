namespace roguelike.environment.entity.statemachine {
    public class HostileEntityDeadState : HostileEntityBaseState {
        public HostileEntityDeadState(HostileEntityStateMachine stateMachine) : base(stateMachine, EntityStates.SEARCH) { }

        public override void EnterState() {
            stateMachine.hostileEntity.IsDead = true;
            stateMachine.animator.SetBool("Death", true);
        }

        public override void UpdateState() {

        }

        public override void ExitState() {}

        public override EntityStates GetNextState() {
            return EntityStates.DEAD;
        }
    }
}