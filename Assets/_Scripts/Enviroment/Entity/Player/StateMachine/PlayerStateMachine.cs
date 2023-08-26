using Roguelike.System.PlayerInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Enviroment.Entity.Player.StateMachine {
    public class PlayerStateMachine : MonoBehaviour {
        [SerializeField] private InputReader _input;
        private CharacterController _chc;

        [SerializeField] private float _playerSpeed;
        [SerializeField] private float _turnSmothTime = 0.125f;

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
                public InputReader Input { get { return _input; } }
                public CharacterController Chc { get { return _chc; } }
                public Vector3 MoveDir { get { return _moveDir; } }
                public float PlayerSpeed { get { return _playerSpeed; } }
                public float TurnSmothTime { get { return _turnSmothTime; } }
                public Camera MainCamera { get { return _mainCamera; } }
                public LayerMask GroundMask { get { return _groundMask; } }
                public bool IsAiming { get { return _isAiming; } }



        // Monobehaviour methods

        private void Awake() {
            _chc = GetComponent<CharacterController>();
            _input.MoveEvent += HandleMove;
            _input.AimEvent += InitiateAim;
            _input.AimCancelEvent += CancelAim;

            _states = new PlayerStateFactory(this);
            _currentState = _states.Idle();
            _currentState.EnterState();
            _mainCamera = Camera.main;
        }

        private void Start() {

        }

        private void Update() {
            _currentState.UpdateStates();
        }



        // My methods

        private void HandleMove(Vector2 dir) { _moveDir = new Vector3(dir.x, 0f, dir.y).normalized; }
        private void InitiateAim() { _isAiming = true; }
        private void CancelAim() { _isAiming = false; }
    }
}