using roguelike.rendering.ui;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.UIElements;
using static roguelike.environment.ui.statemachine.UIStateMachine;

namespace roguelike.environment.ui.statemachine { 
    public class UIPauseState: UIBaseState {
        private GameObject _pauseUIHolder;

        private VisualElement _root;

        public UIPauseState(UIStateMachine uiStateMachine, GameObject pauseUIHolder) : base(uiStateMachine) {
            _pauseUIHolder = pauseUIHolder;
        }

        public override void EnterState() { 
            _pauseUIHolder.SetActive(true);

            _root = _pauseUIHolder.GetComponent<UIDocument>().rootVisualElement;

            _root.Q<Button>("ResumeButton").clicked += ResumeClicked;
            _root.Q<Button>("SettingsButton").clicked += SettingsClicked;
            _root.Q<Button>("QuitButton").clicked += ExitToMenu;

            TranslationManager.TranslateHeader(_root.Q<Label>("PauseHeader"));
            TranslationManager.TranslateHeader(_root.Q<Label>("ResumeButtonHeader"));
            TranslationManager.TranslateHeader(_root.Q<Label>("SettingsButtonHeader"));
            TranslationManager.TranslateHeader(_root.Q<Label>("QuitButtonHeader"));
        }

        public override void ExitState() {
            _pauseUIHolder.SetActive(false);

            _root.Q<Button>("ResumeButton").clicked -= ResumeClicked;
            _root.Q<Button>("SettingsButton").clicked -= SettingsClicked;
            _root.Q<Button>("QuitButton").clicked -= ExitToMenu;

            _root = null;

            SettingsScreenRenderer.Instance.gameObject.SetActive(false);
        }

        public override UIStates GetNextState() {
            return UIStates.NONE;
        }

        public override void UpdateState() { }

        public void ResumeClicked() {
            stateMachine.ForceNone();
        }

        public void SettingsClicked() {
            SettingsScreenRenderer.Instance.gameObject.SetActive(true);
        }

        private void ExitToMenu() {
            stateMachine.ForceNone();
            GameManager.Instance.GoToMainMenu();
        }
    }
}
