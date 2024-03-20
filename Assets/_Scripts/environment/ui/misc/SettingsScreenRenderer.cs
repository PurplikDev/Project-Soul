using roguelike.core.utils;
using roguelike.system.manager;
using roguelike.system.singleton;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class SettingsScreenRenderer : PersistentSingleton<SettingsScreenRenderer> {

        VisualElement root;
        EnumField languageDropdown, healthBarDropdown;
        Slider masterVolume, musicVolume, sfxVolume;
        Toggle healthBarText;
        Button applyButton, closeButton;

        protected override void Awake() {
            base.Awake();
            gameObject.SetActive(false);
        }

        public void OnEnable() {
            root = GetComponent<UIDocument>().rootVisualElement;

            healthBarDropdown = root.Q<EnumField>("HealthBarDropdown");
            languageDropdown = root.Q<EnumField>("LanguageDropdown");

            masterVolume = root.Q<Slider>("MasterVolumeSlider");
            musicVolume = root.Q<Slider>("MusicVolumeSlider");
            sfxVolume = root.Q<Slider>("SFXVolumeSlider");

            healthBarText = root.Q<Toggle>("HealthBarTextToggle");

            applyButton = root.Q<Button>("ApplyButton");
            closeButton = root.Q<Button>("CloseButton");

            masterVolume.value = GameManager.CurrentGameSettings.MasterVolume;
            musicVolume.value = GameManager.CurrentGameSettings.MusicVolume;
            sfxVolume.value = GameManager.CurrentGameSettings.SFXVolume;

            healthBarDropdown.value = GameManager.CurrentGameSettings.HealthBarStyle;
            languageDropdown.value = GameManager.CurrentGameSettings.Lang;

            healthBarText.value = GameManager.CurrentGameSettings.HealthBarText;

            applyButton.clicked += ApplySettings;
            closeButton.clicked += Close;
        }

        public void ApplySettings() {
            GameManager.CurrentGameSettings = new GameSettings(0f, 0f, 0f, (HealthBarStyle)healthBarDropdown.value, healthBarText.value, (TranslationManager.Language)languageDropdown.value);
            GameSettings.GameSettingsChanged.Invoke(GameManager.CurrentGameSettings);
        }

        public void Close() {
            gameObject.SetActive(false);
        }
    }
}