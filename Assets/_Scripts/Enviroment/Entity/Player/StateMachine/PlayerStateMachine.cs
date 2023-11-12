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

        Camera _mainCamera = Camera.main;



        // Getters

        // Conditions

        public bool IsMoving { get { return _moveDir.magnitude > 0.1f; } }



        // Variables

        public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public CharacterController CharController { get { return _charController; } }
        public Vector3 MoveDir { get { return _moveDir; } }
        public Camera MainCamera { get { return _mainCamera; } }
        public Transform transform { get { return _playerEntity.transform; } }



        // Statistics

        public float PlayerSpeed { get { return _playerEntity.Speed.Value; } }
        public float PlayerSprintSpeed { get { return PlayerSpeed * 1.45f; } }


        // Monobehaviour methods

        public PlayerStateMachine(Player player, InputReader input) {
            _playerEntity = player;
            _input = input;
            _charController = player.GetComponent<CharacterController>();

            _input.MoveEvent += HandleMove;

            _states = new PlayerStateFactory(this);
            _currentState = _states.Idle();
            _currentState.EnterState();
        }

        public void UpdateStateMachine() {
            _currentState.UpdateStates();
        }



        // My methods

        private void HandleMove(Vector2 dir) { _moveDir = new Vector3(dir.x, 0f, dir.y).normalized; }
    }
}