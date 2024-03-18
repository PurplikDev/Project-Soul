using roguelike.core.utils;
using roguelike.core.utils.gamedata;
using roguelike.system.manager;
using System.Collections.Generic;
using System.IO;
using Tweens;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.mainmenu {
    public class MainMenuRenderer : MonoBehaviour {
        protected UIDocument document;

        private VisualElement _root, _menuRoot, _saveMenuRoot, _saveSelectionRoot, _saveCreationRoot;
        private ScrollView _saveFileList;
        private Button _playButton, _settingsButton, _quitButton, _newSaveButton, _newSaveCreateButton, _newSaveCancelButton;
        private TextField _characterName;

        private VisualTreeAsset _saveFileElement;

        public AudioSource MainMenuMusicSource;

        private void Awake() {
            document = GetComponent<UIDocument>();
            _saveFileElement = Resources.Load<VisualTreeAsset>("ui/uidocuments/templates/SaveElement");

            _root = document.rootVisualElement;

            _menuRoot = _root.Q<VisualElement>("MainMenuHolder");
            _saveMenuRoot = _root.Q<VisualElement>("SaveMenuHolder");
            _saveSelectionRoot = _saveMenuRoot.Q<VisualElement>("SaveSelectionHolder");
            _saveCreationRoot = _saveMenuRoot.Q<VisualElement>("SaveCreationHolder");

            _saveFileList = _saveMenuRoot.Q<ScrollView>("SaveList");
            _characterName = _saveCreationRoot.Q<TextField>("SaveCreationCharacterNameField");

            _playButton = _menuRoot.Q<Button>("PlayButton");
            _settingsButton = _menuRoot.Q<Button>("SettingsButton");
            _quitButton = _menuRoot.Q<Button>("QuitButton");
            _newSaveButton = _saveMenuRoot.Q<Button>("NewSaveButton");
            _newSaveCreateButton = _saveMenuRoot.Q<Button>("CreateSaveButton");
            _newSaveCancelButton = _saveCreationRoot.Q<Button>("CreateSaveCancelButton");

            Translate(GameManager.CurrentGameSettings);
            GameSettings.GameSettingsChanged += Translate;

            _playButton.clicked += OnPlayButton;
            _settingsButton.clicked += OnSettingsButton;
            _quitButton.clicked += OnQuitButton;
            _newSaveButton.clicked += OnNewSaveButton;
            _newSaveCreateButton.clicked += OnNewSaveCreateButton;
            _newSaveCancelButton.clicked += OnCancelButton;


            Directory.CreateDirectory(Path.GetDirectoryName(GlobalStaticValues.SAVE_PATH));

            var files = Directory.GetFiles(GlobalStaticValues.SAVE_PATH);
            List<GameData> data = new List<GameData>();

            foreach(string save in files) {
                data.Add(SaveFileUtils.GetDataFromFile(save));
            }

            data.Sort(delegate(GameData dataX, GameData dataY) {
                if (dataX == null && dataY == null) return 0;
                else if (dataX == null) return -1;
                else if (dataY == null) return 1;
                else return dataX.CompareTo(dataY);
            });

            foreach (GameData save in data) {
                var newSaveElement = _saveFileElement.Instantiate();
                var newSaveElementController = new SaveElementController();
                newSaveElement.userData = newSaveElementController;
                newSaveElementController.SetupElement(newSaveElement, this);
                newSaveElementController.FillData(save);
                _saveFileList.Add(newSaveElement);
            }
        }

        public void OnPlayButton() {
            if(_saveSelectionRoot.style.visibility != Visibility.Visible) {
                TweenElementOpacity(_saveSelectionRoot, 1);
                TweenElementOpacity(_saveCreationRoot, 0);
            } else {
                TweenElementOpacity(_saveSelectionRoot, 0);
                TweenElementOpacity(_saveCreationRoot, 0);
            }
        }

        public void OnSettingsButton() {
            SettingsScreenRenderer.Instance.gameObject.SetActive(true);
        }

        public void OnQuitButton() {
            Application.Quit();
        }

        public void OnNewSaveButton() {
            if (_saveCreationRoot.style.visibility != Visibility.Visible) {
                TweenElementOpacity(_saveSelectionRoot, 0);
                TweenElementOpacity(_saveCreationRoot, 1);
            }
        }

        public void OnNewSaveCreateButton() {

            StopMusic();

            var newGameData = GameManager.CreateNewSave(_characterName.value);
            GameManager.Instance.LoadSave(newGameData);
            LoadingManager.Instance.LoadScene(2, GameState.TOWN);
        }

        public void OnCancelButton() {
            TweenElementOpacity(_saveSelectionRoot, 1);
            TweenElementOpacity(_saveCreationRoot, 0);
        }

        private void TweenElementOpacity(VisualElement element, float opacity) {
            var tween = new FloatTween {
                duration = 0.5f,
                from = element.style.opacity.value,
                to = opacity,
                easeType = EaseType.ExpoInOut,
                onStart = (_) => {
                    element.style.visibility = Visibility.Visible;
                },
                onUpdate = (_, value) => {
                    element.style.opacity = value;
                },
                onFinally = (_) => {
                    if(opacity == 0) {
                        element.style.visibility = Visibility.Hidden;
                    }
                }
            };

            gameObject.AddTween(tween);
        }

        public void StopMusic() {
            var tween = new FloatTween {
                duration = 1,
                from = MainMenuMusicSource.volume,
                to = 0,
                onUpdate = (_, value) => {
                    MainMenuMusicSource.volume = value;
                }
            };

            gameObject.AddTween(tween);
        }

        private void Translate(GameSettings _) {
            TranslationManager.TranslateHeader(_playButton.Q<Label>());
            TranslationManager.TranslateHeader(_settingsButton.Q<Label>());
            TranslationManager.TranslateHeader(_quitButton.Q<Label>());
            TranslationManager.TranslateHeader(_root.Q<Label>("SavesHeader"));
            TranslationManager.TranslateHeader(_newSaveButton.Q<Label>());
            TranslationManager.TranslateHeader(_saveCreationRoot.Q<Label>("CreateSaveButtonHeader"));
            TranslationManager.TranslateHeader(_saveCreationRoot.Q<Label>("SaveCreationCharacterNameHeader"));
            TranslationManager.TranslateHeader(_saveCreationRoot.Q<Label>("CreateSaveCancelButtonHeader"));
        }
    }
}