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
        internal Animator Animator { get; private set; }


        // PUBLIC

        public bool IsMoving { get; private set; }
        public bool IsSprinting { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public Vector3 GetCurrentMovementSpeed { get { return new Vector3(CurrentMovementInput.x, 0, CurrentMovementInput.y) * Player.Speed.Value; } }
        public Vector3 GetCurrentSprintSpeed { get { return GetCurrentMovementSpeed * 1.45f; } }



        void Awake() {
            Player = GetComponent<Player>();
            Animator = GetComponent<Animator>();
            input = Player.PlayerInput;
            CharacterController = GetComponent<CharacterController>();

            states.Add(PlayerStates.IDLE, new PlayerIdleState(this));
            states.Add(PlayerStates.WALK, new PlayerWalkState(this));
            states.Add(PlayerStates.RUN, new PlayerRunState(this));
            states.Add(PlayerStates.ATTACK, new PlayerAttackState(this));

            currentState = states[PlayerStates.IDLE];
        }

        void OnMovementInput(InputAction.CallbackContext context) {
            CurrentMovementInput = context.ReadValue<Vector2>();
            IsMoving = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;
        }

        void OnSpritingInput(InputAction.CallbackContext context) {
            IsSprinting = context.ReadValueAsButton();
        }

        void OnAttackInput(InputAction.CallbackContext context) {
            var item = (core.item.WeaponItem)Player.ItemInMainHand;
            if(item != null) {
                TransitionToState(PlayerStates.ATTACK);
                StartCoroutine(((PlayerAttackState)currentState).PlayerAttack(item));
            }
        }

        void OnEnable() {
            input.CharacterControls.Enable();

            input.CharacterControls.Movement.started += OnMovementInput;
            input.CharacterControls.Movement.performed += OnMovementInput;
            input.CharacterControls.Movement.canceled += OnMovementInput;

            input.CharacterControls.Sprint.started += OnSpritingInput;
            input.CharacterControls.Sprint.canceled += OnSpritingInput;

            input.EnviromentControls.PrimaryAction.started += OnAttackInput;
        }

        void OnDisable() {
            input.CharacterControls.Disable();

            input.CharacterControls.Movement.started -= OnMovementInput;
            input.CharacterControls.Movement.performed -= OnMovementInput;
            input.CharacterControls.Movement.canceled -= OnMovementInput;

            input.CharacterControls.Sprint.started -= OnSpritingInput;
            input.CharacterControls.Sprint.canceled -= OnSpritingInput;

            input.EnviromentControls.PrimaryAction.started -= OnAttackInput;
        }

        public enum PlayerStates {
            IDLE,
            WALK,
            RUN,
            ATTACK
        }
    }
}