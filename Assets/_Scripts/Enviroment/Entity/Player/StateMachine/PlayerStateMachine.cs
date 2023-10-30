using roguelike.system.input;
using UnityEngine;

namespace roguelike.enviroment.entity.player.StateMachine {
    public class PlayerStateMachine {
        private InputReader _input;

        private CharacterController _charController;

        private Player _playerEntity;

        private Vector3 _moveDir;

        PlayerBaseState _currentState;
        PlayerStateFactory _states;

        [SerializeField] private LayerMask _groundMask;

        Camera _mainCamera;

        private bool _isAiming;



        // Getters and Setters

            // Conditions
                public bool IsMoving { get { return _moveDir.magnitude > 0.1f; } }


            // Variables
                public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
                public CharacterController CharController { get { return _charController; } }
                public Vector3 MoveDir { get { return _moveDir; } }
                public float PlayerSpeed { get { return _playerEntity.Speed.Value; } }
                public float TurnSmothTime { get { return 0.0625f; } }
                public Camera MainCamera { get { return _mainCamera; } }
                public LayerMask GroundMask { get { return _groundMask; } }
                public bool IsAiming { get { return _isAiming; } }

                public Transform transform { get { return _playerEntity.transform; } }

        // Monobehaviour methods

        public PlayerStateMachine(Player player, InputReader input) {
            _playerEntity = player;
            _input = input;
            _charController = player.GetComponent<CharacterController>();

            _input.MoveEvent += HandleMove;
            _input.AimEvent += InitiateAim;
            _input.AimCancelEvent += CancelAim;

            _states = new PlayerStateFactory(this);
            _currentState = _states.Idle();
            _currentState.EnterState();
            _mainCamera = Camera.main;
        }

        public void UpdateStateMachine() {
            _currentState.UpdateStates();
        }



        // My methods

        private void HandleMove(Vector2 dir) { _moveDir = new Vector3(dir.x, 0f, dir.y).normalized; }
        private void InitiateAim() { _isAiming = true; }
        private void CancelAim() { _isAiming = false; }
    }
}