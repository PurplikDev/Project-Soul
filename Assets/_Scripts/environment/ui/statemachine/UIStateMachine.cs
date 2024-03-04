using roguelike.core.statemachine;
using roguelike.environment.entity.npc;
using roguelike.environment.entity.player;
using roguelike.environment.world.deployable;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.InputSystem;
using static roguelike.environment.ui.statemachine.UIStateMachine;

namespace roguelike.environment.ui.statemachine {
    public class UIStateMachine : StateManager<UIStates> {
        internal system.input.PlayerInput input { get; private set; }

        private bool _isInUI = false;

        private UIDeployableState _deployableState;
        private UITraderState _traderState;

        protected override void Start() {
            var player = GetComponent<Player>();

            states.Add(UIStates.NONE, new UINoneState(this));
            states.Add(UIStates.INVENTORY, new UIInventoryState(this, player.InventoryScreen));
            states.Add(UIStates.PAUSE, new UIPauseState(this, player.PauseScreen));

            currentState = states[UIStates.NONE];
            // some sort of interaction with objects logic call here

            input = player.PlayerInput;

            input.UIControls.Inventory.performed += OnInventory;
            input.UIControls.Pause.performed += OnPause;

            base.Start();
        }

        void OnInventory(InputAction.CallbackContext context) {
            if (!_isInUI) {
                TransitionToState(UIStates.INVENTORY);
                _isInUI = true;
            } else {
                TransitionToState(UIStates.NONE);
                _isInUI = false;
            }
        }

        void OnPause(InputAction.CallbackContext context) {
            if(!_isInUI) {
                TransitionToState(UIStates.PAUSE);
                _isInUI = true;
            } else {
                TransitionToState(UIStates.NONE);
                _isInUI = false;
            }
        }

        // todo: remake this so that you can't close deployables when you are in one 

        public void OnDeployable(Deployable deployable) {
            if(!_isInUI) {
                _deployableState = new UIDeployableState(this, deployable);
                states.Add(UIStates.DEPLOYABLE, _deployableState);
                TransitionToState(UIStates.DEPLOYABLE);
                _isInUI = true;
            }
        }

        internal void OnDeployableExit() {
            states.Remove(UIStates.DEPLOYABLE);
            _isInUI = false;
        }

        public void OnTrader(Trader trader) {
            // todo: replace this with an in-world speech bubble
            Debug.Log(trader.InteractMessage);
            if(!_isInUI) {
                _traderState = new UITraderState(this, trader);
                states.Add(UIStates.TRADER, _traderState);
                TransitionToState(UIStates.TRADER);
                _isInUI = true;
            }
        }

        internal void OnTraderExit()
        {
            states.Remove(UIStates.TRADER);
            _isInUI = false;
        }

        protected override void Update() { 
            currentState.UpdateState();
        }

        public enum UIStates {
            NONE,
            INVENTORY,
            PAUSE,
            DEPLOYABLE,
            TRADER
        }
    }
}