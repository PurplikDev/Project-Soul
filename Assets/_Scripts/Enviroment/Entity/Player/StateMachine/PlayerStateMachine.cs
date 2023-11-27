using roguelike.core.statemachine;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.InputSystem;
using static roguelike.enviroment.entity.player.statemachine.PlayerStateMachine;

namespace roguelike.enviroment.entity.player.statemachine {
    public class PlayerStateMachine : StateManager<PlayerStates> {

        // PRIVATE/DEFAULT/PROTECTED/INTERNAL

        Vector2 CurrentMovementInput;
        internal system.input.PlayerInput input { get; private set; }
        internal Player Player { get; private set; }


        // PUBLIC

        public bool IsMoving { get; private set; }
        public bool IsSprinting { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public Vector3 GetCurrentMovementSpeed { get { return new Vector3(CurrentMovementInput.x, 0, CurrentMovementInput.y) * Player.Speed.Value; } }
        public Vector3 GetCurrentSprintSpeed { get { return GetCurrentMovementSpeed * 1.45f; } }



        void Awake() {
            input = GameManager.Instance.Input;
            CharacterController = GetComponent<CharacterController>();
            Player = GetComponent<Player>();

            states.Add(PlayerStates.IDLE, new PlayerIdleState(this));
            states.Add(PlayerStates.WALK, new PlayerWalkState(this));
            states.Add(PlayerStates.RUN, new PlayerRunState(this));

            currentState = states[PlayerStates.IDLE];

            input.CharacterControls.Movement.started += OnMovementInput;
            input.CharacterControls.Movement.performed += OnMovementInput;
            input.CharacterControls.Movement.canceled += OnMovementInput;

            input.CharacterControls.Sprint.started += OnSpritingInput;
            input.CharacterControls.Sprint.canceled += OnSpritingInput;
        }

        void OnMovementInput(InputAction.CallbackContext context) {
            CurrentMovementInput = context.ReadValue<Vector2>();
            IsMoving = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;
        }

        void OnSpritingInput(InputAction.CallbackContext context) {
            IsSprinting = context.ReadValueAsButton();
        }

        void OnEnable() {
            input.CharacterControls.Enable();
        }

        void OnDisable() {
            input.CharacterControls.Disable();
        }

        public enum PlayerStates {
            IDLE,
            WALK,
            RUN
        }
    }
}