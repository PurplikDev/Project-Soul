using UnityEngine;

namespace roguelike.enviroment.entity.player.StateMachine {
    public class PlayerAimState : PlayerBaseState {

        float _turnSmoothVelocity;

        public PlayerAimState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) {
            InitializeSubState();
        }

        public override void EnterState() {

        }

        public override void UpdateState() {
            HandleMovement();
            Aim();
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
            Ctx.CharController.Move(Ctx.MoveDir * Time.deltaTime * Ctx.PlayerSpeed);
        }

        private void Aim() {
            var (success, position) = GetMousePosition();
            if (success) {
                var direction = position - Ctx.transform.position;
                direction.y = 0;
                Ctx.transform.forward = direction;
            }
        }

        private (bool success, Vector3 position) GetMousePosition() {
            var ray = Ctx.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, Ctx.GroundMask)) {
                return (success: true, position: hitInfo.point);
            } else {
                return (success: false, position: Vector3.zero);
            }
        }
    }
}