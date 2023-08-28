using UnityEngine;

namespace roguelike.enviroment.entity.player.StateMachine {
    public class PlayerWalkState : PlayerBaseState {

        float _turnSmoothVelocity;

        public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
            InitializeSubState();
        }

        public override void EnterState() {
            //_ctx.Animator.SetBool("walk", true);
            Debug.Log("Entered walk");
        }

        public override void UpdateState() {
            HandleMovement();
            CheckSwitchStates();
        }

        public override void ExitState() {

        }

        public override void InitializeSubState() {

        }

        public override void CheckSwitchStates() {
            if (!Ctx.IsMoving) {
                SwitchState(Factory.Idle());
            }
        }

        void HandleMovement() {
            float targetAngle = Mathf.Atan2(Ctx.MoveDir.x, Ctx.MoveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(Ctx.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, 0.125f);

            Ctx.transform.rotation = Quaternion.Euler(0, angle, 0);
            Ctx.Chc.Move(Ctx.MoveDir * Time.deltaTime * Ctx.PlayerSpeed);
        }
    }
}