using roguelike.core.statemachine;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace roguelike.environment.entity.player.statemachine {
    public class PlayerStateMachine : StateManager<PlayerStates> {

        // PRIVATE/DEFAULT/PROTECTED/INTERNAL

        Vector2 CurrentMovementInput;

        internal bool facingRight = false;
        internal system.input.PlayerInput input { get; private set; }
        internal Player player { get; private set; }
        internal Animator animator { get; private set; }
        


        // PUBLIC

        public bool IsMoving { get; private set; }
        public bool IsSprinting { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public Vector3 GetCurrentMovementSpeed { get { return new Vector3(CurrentMovementInput.x, 0, CurrentMovementInput.y) * player.Speed.Value; } }
        public Vector3 GetCurrentSprintSpeed { get { return GetCurrentMovementSpeed * 1.45f; } }



        void Awake() {
            player = GetComponent<Player>();
            animator = GetComponent<Animator>();
            input = player.PlayerInput;
            CharacterController = GetComponent<CharacterController>();

            states.Add(PlayerStates.IDLE, new PlayerIdleState(this));
            states.Add(PlayerStates.WALK, new PlayerWalkState(this));
            // states.Add(PlayerStates.RUN, new PlayerRunState(this));
            states.Add(PlayerStates.ATTACK, new PlayerAttackState(this));

            currentState = states[PlayerStates.IDLE];
        }

        protected override void Update() {
            var direction = GetCurrentMovementSpeed.x > 0 && !(GetCurrentMovementSpeed.x < 0);
            if (direction != facingRight) {
                facingRight = direction;
                animator.SetBool("FacingRight", direction);
                animator.SetTrigger("WalkTrigger");
            }

            base.Update();
        }

        void OnMovementInput(InputAction.CallbackContext context) {
            CurrentMovementInput = context.ReadValue<Vector2>().normalized;
            IsMoving = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;
        }

        void OnSpritingInput(InputAction.CallbackContext context) {
            IsSprinting = context.ReadValueAsButton();
        }

        void OnAttackInput(InputAction.CallbackContext context) {
            var item = (core.item.WeaponItem)player.ItemInMainHand;
            if(item != null && !(currentState is PlayerAttackState)) {
                TransitionToState(PlayerStates.ATTACK);
                StartCoroutine(((PlayerAttackState)currentState).PlayerAttack(item));
            }
        }

        void OnEnable() {
            input.CharacterControls.Enable();

            input.CharacterControls.Movement.started += OnMovementInput;
            input.CharacterControls.Movement.performed += OnMovementInput;
            input.CharacterControls.Movement.canceled += OnMovementInput;

            //input.CharacterControls.Sprint.started += OnSpritingInput;
            //input.CharacterControls.Sprint.canceled += OnSpritingInput;

            input.EnviromentControls.PrimaryAction.started += OnAttackInput;
        }

        void OnDisable() {
            input.CharacterControls.Disable();

            input.CharacterControls.Movement.started -= OnMovementInput;
            input.CharacterControls.Movement.performed -= OnMovementInput;
            input.CharacterControls.Movement.canceled -= OnMovementInput;

            //input.CharacterControls.Sprint.started -= OnSpritingInput;
            //input.CharacterControls.Sprint.canceled -= OnSpritingInput;

            input.EnviromentControls.PrimaryAction.started -= OnAttackInput;
        }
    }

    public enum PlayerStates {
        IDLE,
        WALK,
        RUN,
        ATTACK
    }
}