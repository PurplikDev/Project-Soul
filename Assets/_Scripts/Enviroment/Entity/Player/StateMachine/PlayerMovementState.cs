using UnityEngine;

namespace roguelike.enviroment.entity.player.StateMachine {
    public class PlayerMovementState : PlayerBaseState {
        public PlayerMovementState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
            IsRootState = true;
            InitializeSubState();
        }
        public override void EnterState() {

        }

        public override void UpdateState() {
            CheckSwitchStates();
        }

        public override void ExitState() {

        }

        public override void InitializeSubState() {
	        if(Ctx.IsAiming) {
		        SetSubState(Factory.Aim());
	        } else {
                SetSubState(Factory.Walk());
	        }
        }

        public override void CheckSwitchStates() {
            if (!Ctx.IsMoving) {
                SwitchState(Factory.Idle());
            }
        }
    }
}