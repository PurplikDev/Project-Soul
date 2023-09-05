using roguelike.enviroment.item;
using roguelike.system.input;
using System;
using UnityEngine;

namespace roguelike.system.gamemanager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private InputReader _input;
        [SerializeField] private GameObject pauseMenu;

        private void Awake() {
            TranslationManager.getTranslationFromFile();
        }

        private void Start()
        {
            // Gameplay UI Events
            _input.PauseEvent += HandlePause;
            _input.InventoryEvent += HandleInventory;

            // UI Events
            _input.CloseUIEvent += CloseAllUI;
            _input.CloseInvetoryEvent += CloseInventory;

            Debug.Log(Items.TEST_ITEM.ItemName);
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