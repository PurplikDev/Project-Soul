using roguelike.core.utils;
using roguelike.environment.entity;
using roguelike.environment.entity.player;
using roguelike.rendering.ui.slot;
using roguelike.system.manager;
using System;
using System.Collections.Generic;
using Tweens;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace roguelike.environment.ui.hud {
    public class HealthBarRenderer : MonoBehaviour {

        public Entity Target { get; private set; }
        public HealthBarStyle HealthBarStyle;
        public bool HealthDisplayText, ShouldReveal, keepHidden;

        private float oldMaxHealth = 0;

        private UIDocument HealthDisplay;

        // HEART DISPLAY

        private VisualElement _heartDisplayHolder;
        private List<Heart> _hearts = new List<Heart>();

        // BAR DISPLAY

        private VisualElement _barDisplayHolder, _barFill, _barFillEffect;
        private Label _barHealthDisplay;


        public void SetTarget(Entity target) {
            Target = target;
            Target.HealthUpdate += UpdateHealthDisplay;
            Target.MaxHealthUpdate += UpdateHealthDisplay;
            Target.DeathEvent += HideHealthDisplay;
        }

        public void InitiateRenderer() {
            HealthDisplay = GetComponent<UIDocument>();

            var root = HealthDisplay.rootVisualElement;

            _heartDisplayHolder = root.Q<VisualElement>("HeartDisplay");

            _barDisplayHolder = root.Q<VisualElement>("HealthBarHolder");
            _barFill = _barDisplayHolder.Q<VisualElement>("HealthBarFill");
            _barFillEffect = _barDisplayHolder.Q<VisualElement>("HealthBarWhite");
            _barHealthDisplay = _barDisplayHolder.Q<Label>("HealthBarText");

            if(Target is Player) {
                GameSettings.GameSettingsChanged += DisplaySettingsUpdated;

                HealthBarStyle = GameManager.CurrentGameSettings.HealthBarStyle;
                HealthDisplayText = GameManager.CurrentGameSettings.HealthBarText;
            }

            if (ShouldReveal) {
                _heartDisplayHolder.style.opacity = 0;
                _barHealthDisplay.style.opacity = 0;
                _barDisplayHolder.style.width = 0;
            }
        }

        private void Start() {
            UpdateHealthDisplay();

            if (ShouldReveal) {
                switch (HealthBarStyle) {
                    case HealthBarStyle.CLASSIC:
                        // RevealHearts(); NOT IMPLEMENTED YET
                        break;
                    case HealthBarStyle.BAR:
                        RevealBar();
                        break;
                    default: Debug.LogError("No valid HealthBarStyle!"); break;
                }
            }
        }

        private void OnDestroy() {
            Target.HealthUpdate -= UpdateHealthDisplay;
            Target.MaxHealthUpdate -= UpdateHealthDisplay;
            Target.DeathEvent -= HideHealthDisplay;
            GameSettings.GameSettingsChanged -= DisplaySettingsUpdated;
        }

        private void DisplaySettingsUpdated(GameSettings settings) {
            HealthBarStyle = settings.HealthBarStyle;
            HealthDisplayText = settings.HealthBarText;

            Debug.Log("whar");

            UpdateHealthDisplay();
        }

        private void UpdateHealthDisplay() {
            switch(HealthBarStyle) {
                case HealthBarStyle.CLASSIC: 
                    UpdateHearts(); 
                    _heartDisplayHolder.style.visibility = Visibility.Visible;
                    _barDisplayHolder.style.visibility = Visibility.Hidden;
                    break;
                case HealthBarStyle.BAR: 
                    UpdateBar();
                    DestroyHearts();
                    _heartDisplayHolder.style.visibility = Visibility.Hidden;
                    _barDisplayHolder.style.visibility = Visibility.Visible;
                    break;
                default: Debug.LogError("No valid HealthBarStyle!"); break;
            }
        }

        private void HideHealthDisplay() {
            _heartDisplayHolder.style.visibility = Visibility.Hidden;
            _barDisplayHolder.style.visibility = Visibility.Hidden;
        }

        // HEART DISPLAY METHODS

        private void RenderHearts() {
            oldMaxHealth = Target.MaxHealth.Value;

            for(float i = oldMaxHealth; i > 0; i -= 10) {
                var heart = new Heart();
                _hearts.Add(heart);
                _heartDisplayHolder.Add(heart);
            }
        }

        private void UpdateHearts() {
            _hearts.Clear();
            _heartDisplayHolder.Clear();
            RenderHearts();

            float healthToDistribute = Target.Health;
            foreach (Heart heart in _hearts) {
                if (healthToDistribute >= 10) {
                    heart.UpdateHeart(10, HealthDisplayText);
                    healthToDistribute -= 10;
                } else if(healthToDistribute < 10 && healthToDistribute > 0) {
                    heart.UpdateHeart(healthToDistribute, HealthDisplayText);
                    healthToDistribute = 0;
                } else {
                    heart.UpdateHeart(0, HealthDisplayText);
                }
            }
        }

        private void DestroyHearts() {
            foreach(Heart heart in _hearts) {
                heart.UpdateHeart(0, false);
            }
        }

        // BAR DISPLAY METHODS

        private void UpdateBar() {
            if(HealthDisplayText) {
                _barHealthDisplay.text = $"{Target.Health}/{Target.MaxHealth.Value}";
            } else {
                _barHealthDisplay.style.visibility = Visibility.Hidden;
            }
            _barFill.style.width = new StyleLength(new Length(Target.Health / Target.MaxHealth.Value * 100, LengthUnit.Percent));
            BarDamageEffect();
        }

        public void RevealBar() {
            var tween = new FloatTween {
                duration = 2,
                from = 0,
                to = 100,
                easeType = EaseType.ExpoInOut,
                onUpdate = (_, value) => {
                    _barDisplayHolder.style.width = new StyleLength(new Length(value, LengthUnit.Percent));
                },
                onFinally = (_) => {
                    RevealText();
                }
            };
            gameObject.AddTween(tween);
        }

        private void RevealText() {
            var tween = new FloatTween {
                duration = 0.5f,
                from = 0,
                to = 1,
                onUpdate = (_, value) => {
                    _barHealthDisplay.style.opacity = value;
                }
            };
            gameObject.AddTween(tween);
        }

        private void BarDamageEffect() {
            var tween = new FloatTween {
                duration = 0.75f,
                from = _barFillEffect.style.width.value.value,
                to = _barFill.style.width.value.value,
                easeType = EaseType.ExpoInOut,
                onUpdate = (_, value) => {
                    _barFillEffect.style.width = new StyleLength(new Length(value, LengthUnit.Percent));
                }
            };
            gameObject.AddTween(tween);
        }
    }

    public class Heart : VisualElement {
        private Label _heartText;

        public Heart() {
            _heartText = new Label();

            Add(_heartText);
            AddToClassList("heart");

            _heartText.style.visibility = Visibility.Hidden;
            UpdateHeart(10, false);
        }

        public void UpdateHeart(float value, bool showText) {
            style.backgroundImage = Resources.Load<Sprite>($"sprites/ui/hud/heart{value}").texture;
            if(value < 10 && value > 0 && showText) {
                _heartText.text = value.ToString();
                _heartText.style.visibility = Visibility.Visible;
            } else {
                _heartText.style.visibility = Visibility.Hidden;
            }
        }

        #region UXML
        [Preserve]
        public new class UxmlFactory : UxmlFactory<Heart, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}