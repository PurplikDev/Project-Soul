using Roguelike.System.PlayerInput;
using UnityEngine;

namespace Roguelike.System.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputReader _input;
        [SerializeField] private GameObject pauseMenu;

        private void Start()
        {
            // Gameplay UI Events
            _input.PauseEvent += HandlePause;
            _input.InventoryEvent += HandleInventory;

            // UI Events
            _input.CloseUIEvent += CloseAllUI;
            _input.CloseInvetoryEvent += CloseInventory;
        }

        private void HandlePause()
        {
            pauseMenu.SetActive(true);
        }

        // TODO: Correct implementation, current one is only for testing
        private void HandleInventory()
        {
            pauseMenu.SetActive(true);
        }

        // TODO: Correct implementation, current one is only for testing
        private void CloseAllUI()
        {
            pauseMenu.SetActive(false);
        }

        // TODO: Correct implementation, current one is only for testing
        private void CloseInventory()
        {
            pauseMenu.SetActive(false);
        }
    }
}