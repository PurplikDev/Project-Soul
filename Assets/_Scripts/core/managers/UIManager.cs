using System.Linq;
using roguelike.system.input;
using roguelike.system.singleton;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.system.manager {
    public class UIManager : Singleton<UIManager> {
        private InputReader _inputReader;

        public static UIDocument PlayerInventoryUI;
        public static UIDocument PauseUI;

        protected override void Awake() {
            _inputReader = Resources.LoadAll<InputReader>("data/player").First();

            _inputReader.CloseUIEvent += CloseAll;
            _inputReader.InventoryEvent += HandleInventory;
            _inputReader.PauseEvent += HandlePause;

            base.Awake();
        }

        private void HandleInventory()
        {
            SetState(PlayerInventoryUI, PlayerInventoryUI.gameObject.activeSelf);
        }

        private void HandlePause()
        {
            if(PauseUI.gameObject.activeSelf)
            {
                // todo: check if any ui is open and lose it, if none are open open the pause menu
            }
        }

        private void CloseAll()
        {
            SetState(PlayerInventoryUI, false);
            SetState(PauseUI, false);
        }

        private void SetState(UIDocument document, bool state)
        {
            document.gameObject.SetActive(state);
        }

        
    }
}