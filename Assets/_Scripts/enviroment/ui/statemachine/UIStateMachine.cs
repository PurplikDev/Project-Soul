using roguelike.core.statemachine;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.InputSystem;
using static roguelike.enviroment.ui.statemachine.UIStateMachine;

namespace roguelike.enviroment.ui.statemachine {
    public class UIStateMachine : StateManager<UIStates> {
        internal system.input.PlayerInput input { get; private set; }

        private bool _isInventory = false;
        private bool _isPause = false;

        public GameObject InventoryUIHolder;
        public GameObject PauseUIHolder;

        void Awake() {
            input = GameManager.Instance.Input;

            states.Add(UIStates.NONE, new UINoneState(this));
            states.Add(UIStates.INVENTORY, new UIInventoryState(this, InventoryUIHolder));
            states.Add(UIStates.PAUSE, new UIPauseState(this, PauseUIHolder));

            currentState = states[UIStates.NONE];
            // some sort of interaction with objects logic call here

            input.UIControls.Inventory.performed += OnInventory;
            input.UIControls.Pause.performed += OnPause;
        }

        void OnInventory(InputAction.CallbackContext context) {
            if (!_isInventory && !_isPause) {
                TransitionToState(UIStates.INVENTORY);
                _isInventory = true;
            } else if(_isInventory){
                TransitionToState(UIStates.NONE);
                _isInventory = false;
            }
        }

        void OnPause(InputAction.CallbackContext context) {
            if(_isPause || _isInventory) {
                TransitionToState(UIStates.NONE);
                _isInventory = false;
                _isPause = false;
            } else {
                TransitionToState(UIStates.PAUSE);
                _isPause = true;
            }
        }

        void OnEnable() {
            input.UIControls.Enable();
        }

        void OnDisable() {
            input.UIControls.Disable();
        }

        protected override void Update() { 
            currentState.UpdateState();
        }

        public enum UIStates {
            NONE,
            INVENTORY,
            PAUSE
        }
    }
}