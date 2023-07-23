using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Purplik.PixelRoguelike.Input
{
    [CreateAssetMenu(menuName = "Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
    {
        private GameInput _gameInput;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Gameplay.SetCallbacks(this);
                _gameInput.UI.SetCallbacks(this);

                SetGameplay();
            }
        }

        /// <summary>
        /// Method that enables Gameplay input and disabled UI input.
        /// </summary>
        public void SetGameplay()
        {
            _gameInput.Gameplay.Enable();
            _gameInput.UI.Disable();
        }

        /// <summary>
        /// Method that enables UI input and disabled Gameplay input.
        /// </summary>
        public void SetUI()
        {
            _gameInput.Gameplay.Disable();
            _gameInput.UI.Enable();
        }

        // Gameplay Events
        public event Action<Vector2> MoveEvent;
        public event Action<Vector2> MouseAimEvent;

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            MouseAimEvent?.Invoke(context.ReadValue<Vector2>());
        }



        // Gameplay UI Events
        public event Action InventoryEvent;
        public event Action PauseEvent;

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                SetUI();
            }
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                InventoryEvent?.Invoke();
                SetUI();
            }
        }



        // UI Events
        public event Action CloseUIEvent;
        public event Action CloseInvetoryEvent;

        public void OnCloseAllUI(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                CloseUIEvent?.Invoke();
                SetGameplay();
            }
        }

        public void OnCloseInventory(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                CloseInvetoryEvent?.Invoke();
                SetGameplay();
            }
        }
    }        
}