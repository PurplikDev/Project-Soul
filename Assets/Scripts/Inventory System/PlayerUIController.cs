using io.purplik.ProjectSoul.Entity;
using io.purplik.ProjectSoul.Entity.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class PlayerUIController : MonoBehaviour
    {
        [Header("<color=#6F90FF>Inventory")]
        [SerializeField] GameObject inventoryPanel;
        [SerializeField] GameObject equipmentPanel;
        [SerializeField] GameObject statsPanel;
        [Space]
        [Header("<color=red>Misc UIs")]
        [SerializeField] GameObject pauseMenu;

        private GameObject[] allElements;

        private void OnValidate()
        {
            inventoryPanel = GameObject.Find("_InventoryPanel");
            equipmentPanel = GameObject.Find("_EquipmentPanel");
            statsPanel = GameObject.Find("_StatsPanel");

            pauseMenu = GameObject.Find("_PausePanel");

            allElements = new GameObject[] { inventoryPanel, equipmentPanel, statsPanel, pauseMenu };
        }

        private void Awake()
        {
            pauseMenu.SetActive(false);
        }

        void Update()
        {
            // Close UI elements
            if (Input.GetKeyDown(PlayerKeybinds.openInventory) || Input.GetKeyDown(PlayerKeybinds.pauseGame))
            {
                if(AnyUIActive())
                {
                    ToggleAllUI(false);
                    return;
                }
            }

            // Open Inventory when player presses coresponding keybind
            if (Input.GetKeyDown(PlayerKeybinds.openInventory))
            {
                ToggleInventoryAll(true);
                return;
            }

            // Pause game when player presses coresponding keybind
            if (Input.GetKeyDown(PlayerKeybinds.pauseGame))
            {
                ToggleUIElement(!pauseMenu.activeSelf, pauseMenu);
                return;
            }

            // Check if a player is near an interactable tile
            if (Input.GetKeyDown(PlayerKeybinds.secondaryAction) && !Input.GetKey(PlayerKeybinds.crouch) && !AnyUIActive())
            {
                RaycastHit hit;
                Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f);
                if(hit.transform == null) { return; }
                var tileEntity = hit.transform.gameObject.GetComponent<ITileEntity>();
                if(tileEntity != null) { 
                    tileEntity.Interact();
                    ToggleUIElement(true, inventoryPanel);
                }
                return;
            }
        }

        public void ToggleUIElement(bool toggle, GameObject element)
        {
            if (toggle)
            {
                Cursor.lockState = CursorLockMode.None;
                
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Camera.main.GetComponent<PlayerCamera>().lockRotation = toggle;
            Cursor.visible = toggle;
            element.SetActive(toggle);
        }

        public void ToggleInventoryAll(bool toggle)
        {
            ToggleUIElement(toggle, inventoryPanel);
            ToggleUIElement(toggle, equipmentPanel);
            ToggleUIElement(toggle, statsPanel);
        }

        public void ToggleAllUI(bool toggle)
        {
            for (int i = 0; i < allElements.Length; i++)
            {
                ToggleUIElement(toggle, allElements[i]);
            }
        }

        private bool AnyUIActive()
        {
            for(int i = 0; i < allElements.Length; i++)
            {
                if(allElements[i].gameObject.activeSelf)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
