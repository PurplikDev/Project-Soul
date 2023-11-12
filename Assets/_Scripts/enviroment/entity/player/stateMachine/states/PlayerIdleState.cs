using UnityEngine;

namespace roguelike.enviroment.entity.player.StateMachine {
    public class PlayerIdleState : PlayerBaseState {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
            IsRootState = true;
            InitializeSubState();
        }

        public override void EnterState() {
            //Debug.Log("Entered idle");
        }

        public override void UpdateState() {
            CheckSwitchStates();
        }

        public override void ExitState() {

        }

        public override void InitializeSubState() {

        }

        public override void CheckSwitchStates() {
            if (Ctx.IsMoving) {
                SwitchState(Factory.Movement());
            }
        }
    }
}