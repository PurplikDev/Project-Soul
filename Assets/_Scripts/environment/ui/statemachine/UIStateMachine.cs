using roguelike.core.statemachine;
using roguelike.environment.world.deployable;
using roguelike.system.manager;
using UnityEngine.InputSystem;
using static roguelike.environment.ui.statemachine.UIStateMachine;

namespace roguelike.environment.ui.statemachine {
    public class UIStateMachine : StateManager<UIStates> {
        internal system.input.PlayerInput input { get; private set; }

        private bool _isInventory = false;
        private bool _isPause = false;
        private bool _isDeployable = false;

        private UIDeployableState _deployableState;

        void Awake() {
            input = GameManager.Instance.Input;

            var player = GameManager.Instance.Player;

            states.Add(UIStates.NONE, new UINoneState(this));
            states.Add(UIStates.INVENTORY, new UIInventoryState(this, player.InventoryScreen));
            states.Add(UIStates.PAUSE, new UIPauseState(this, player.PauseScreen));

            currentState = states[UIStates.NONE];
            // some sort of interaction with objects logic call here

            input.UIControls.Inventory.performed += OnInventory;
            input.UIControls.Pause.performed += OnPause;
        }

        void OnInventory(InputAction.CallbackContext context) {
            if (!_isInventory && !_isPause && !_isDeployable) {
                TransitionToState(UIStates.INVENTORY);
                _isInventory = true;
                _isDeployable = false;
            } else if(!_isPause){
                TransitionToState(UIStates.NONE);
                _isInventory = false;
            }
        }

        void OnPause(InputAction.CallbackContext context) {
            if(_isPause || _isInventory || _isDeployable) {
                TransitionToState(UIStates.NONE);
                _isInventory = false;
                _isPause = false;
                _isDeployable = false;
            } else {
                TransitionToState(UIStates.PAUSE);
                _isPause = true;
            }
        }

        // todo: remake this so that you can't close deployables when you are in one 

        public void OnDeployable(Deployable deployable) {
            if(!_isInventory && !_isPause && !_isDeployable) {
                _deployableState = new UIDeployableState(this, deployable);
                states.Add(UIStates.DEPLOYABLE, _deployableState);
                TransitionToState(UIStates.DEPLOYABLE);
                _isDeployable = true;
            }
        }

        internal void OnDeployableExit() {
            states.Remove(UIStates.DEPLOYABLE);
            _isDeployable = false;
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
            PAUSE,
            DEPLOYABLE
        }
    }
}