using System.Collections.Generic;
using System.Linq;
using roguelike.system.input;
using roguelike.system.singleton;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.system.manager {
    public class UIManager : Singleton<UIManager> {
        private InputReader _inputReader;

        public UIDocument MiscUI;
        public UIDocument PauseUI;

        private List<UIDocument> activeUI = new List<UIDocument>();

        protected override void Awake() {
            _inputReader = Resources.LoadAll<InputReader>("data/player").First();

            _inputReader.CloseUIEvent += CloseAll;
            _inputReader.PauseEvent += HandlePause;

            base.Awake();
        }

        private void HandlePause()
        {
            
        }

        private void CloseAll()
        {
            foreach (UIDocument document in activeUI) {
                document.enabled = false;
            }
            activeUI.Clear();
        }

        private void ToggleElement(bool toggle, UIDocument document) {
            document.enabled = toggle;
            if(toggle) {
                activeUI.Add(document);
            }
        }
    }
}