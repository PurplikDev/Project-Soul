using roguelike.core.utils;
using roguelike.system.manager;
using roguelike.system.singleton;
using System.Collections.Generic;
using Tweens;
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

        Dictionary<string, Label> labels;

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

            var masterHeader = root.Q<Label>("MasterSliderHeader");
            var musicHeader = root.Q<Label>("MusicSliderHeader");
            var sfxHeader = root.Q<Label>("SFXSliderHeader");
            var settingsHeader = root.Q<Label>("SettingsHeader");
            var barHeader = root.Q<Label>("BarStyleHeader");
            var barTextHeader = root.Q<Label>("HealthBarTextLabel");
            var languageHeader = root.Q<Label>("LanguageHeader");
            var closeButtonHeader = root.Q<Label>("CloseButtonHeader");
            var applyButtonHeader = applyButton.Q<Label>();

            labels = new Dictionary<string, Label> {
                { applyButtonHeader.text, applyButtonHeader },
                { masterHeader.text, masterHeader },
                { musicHeader.text, musicHeader },
                { sfxHeader.text, sfxHeader },
                { settingsHeader.text, settingsHeader },
                { barHeader.text, barHeader },
                { barTextHeader.text, barTextHeader },
                { languageHeader.text, languageHeader },
                { closeButtonHeader.text, closeButtonHeader }
            };

            Translate(GameManager.CurrentGameSettings);

            masterVolume.value = GameManager.CurrentGameSettings.MasterVolume;
            musicVolume.value = GameManager.CurrentGameSettings.MusicVolume;
            sfxVolume.value = GameManager.CurrentGameSettings.SFXVolume;

            healthBarDropdown.value = GameManager.CurrentGameSettings.HealthBarStyle;
            languageDropdown.value = GameManager.CurrentGameSettings.Lang;

            healthBarText.value = GameManager.CurrentGameSettings.HealthBarText;

            applyButton.clicked += ApplySettings;
            closeButton.clicked += Close;

            root.style.opacity = 0;

            Reveal(1f);
        }

        private void Translate(GameSettings _) {
            foreach (var element in labels) {
                TranslationManager.TranslateHeader(element.Value, element.Key);
            }
        }

        public void ApplySettings() {
            GameManager.CurrentGameSettings = new GameSettings(masterVolume.value, musicVolume.value, sfxVolume.value, (HealthBarStyle)healthBarDropdown.value, healthBarText.value, (TranslationManager.Language)languageDropdown.value);
            GameSettings.GameSettingsChanged.Invoke(GameManager.CurrentGameSettings);
            Translate(GameManager.CurrentGameSettings);
        }

        public void Close() {
            Reveal(0f);
        }

        private void Reveal(float value) {
            var tween = new FloatTween {
                duration = 0.25f,
                from = root.style.opacity.value,
                to = value,
                onUpdate = (_, value) => {
                    root.style.opacity = value;
                },
                onFinally = (_) => {
                    if(value == 0f) { gameObject.SetActive(false); }
                }
            };

            gameObject.AddTween(tween);
        }
    }
}