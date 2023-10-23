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
        [SerializeField] private GameObject inventoryMenu;

        private void Awake() {
            TranslationManager.getTranslationFromFile();
        }

        private void Start()
        {
            // Gameplay UI Events
            _input.PauseEvent += HandlePause;
            _input.InventoryEvent += HandleInventory;
	        _input.AdjustCameraEvent += UnlockCamera;
	        _input.AdjustCameraCancelEvent += LockCamera;

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
            inventoryMenu.SetActive(true);
        }

        // TODO: Correct implementation, current one is only for testing
        private void CloseAllUI()
        {
            pauseMenu.SetActive(false);
            inventoryMenu.SetActive(false);
        }

        // TODO: Correct implementation, current one is only for testing
        private void CloseInventory()
        {
            inventoryMenu.SetActive(false);
        }

	private void UnlockCamera() {
	    Debug.Log("Camera Unlocked");
    	}

	private void LockCamera() {
	    Debug.Log("Camera Locked");
    	}
    }
}