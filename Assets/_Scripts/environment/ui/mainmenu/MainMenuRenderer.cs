using roguelike.core.utils;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.mainmenu {
    public class MainMenuRenderer : MonoBehaviour {
        protected UIDocument document;

        private VisualElement _root, _menuRoot, _saveMenuRoot;
        private ScrollView _saveFileList;
        private Button _playButton, _settingsButton, _quitButton, _newSaveButton;

        private VisualTreeAsset _saveFileElement;

        private void Awake() {
            document = GetComponent<UIDocument>();
            _saveFileElement = Resources.Load<VisualTreeAsset>("ui/uidocuments/templates/SaveElement");

            _root = document.rootVisualElement;

            _menuRoot = _root.Q<VisualElement>("MainMenuHolder");
            _saveMenuRoot = _root.Q<VisualElement>("SaveMenuHolder");
            _saveFileList = _saveMenuRoot.Q<ScrollView>("SaveList");

            _playButton = _menuRoot.Q<Button>("PlayButton");
            _settingsButton = _menuRoot.Q<Button>("SettingsButton");
            _quitButton = _menuRoot.Q<Button>("QuitButton");
            _newSaveButton = _saveMenuRoot.Q<Button>("NewSaveButton");

            TranslateHeader(_playButton.Q<Label>());
            TranslateHeader(_settingsButton.Q<Label>());
            TranslateHeader(_quitButton.Q<Label>());
            TranslateHeader(_root.Q<Label>("SavesHeader"));
            TranslateHeader(_newSaveButton.Q<Label>());

            _playButton.clicked += OnPlayButton;
            _settingsButton.clicked += OnSettingsButton;
            _quitButton.clicked += OnQuitButton;
            _newSaveButton.clicked += OnNewSaveButton;

            foreach (var save in Directory.GetFiles(GlobalStaticValues.SAVE_PATH)) {
                var newSaveElement = _saveFileElement.Instantiate();
                var newSaveElementController = new SaveElementController();
                newSaveElement.userData = newSaveElementController;
                newSaveElementController.SetupElement(newSaveElement);
                newSaveElementController.FillData(SaveFileUtils.GetDataFromFile(save));
                _saveFileList.Add(newSaveElement);
            }
        }

        public void OnPlayButton() {
            if(_saveMenuRoot.style.visibility != Visibility.Visible) {
                _saveMenuRoot.style.visibility = Visibility.Visible;
            } else {
                _saveMenuRoot.style.visibility = Visibility.Hidden;
            }
        }

        public void OnSettingsButton() {
            Debug.LogWarning("Open settings menu here!");
        }

        public void OnQuitButton() {
            Application.Quit();
        }

        public void OnNewSaveButton() {
            Debug.LogWarning("Create new save!");
        }

        public void OnSaveClicked(PointerDownEvent evt) {
            
        }
        protected void TranslateHeader(Label label) {
            label.text = TranslationManager.getTranslation(label.text);
        }
    }
}