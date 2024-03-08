using roguelike.environment.entity.combat;
using Tweens;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.entity {
    public class BossEntity : HostileEntity {
        private UIDocument _bossHealthBar;
        private VisualElement _root, _healthBar, _healthBarFill, _healthBarWhiteFill;
        private Label _healthText, _bossName, _bossDescription;

        [Header("Boss Information", order = -1)]
        public string BossName;
        public string BossDescription;

        protected override void Awake() {
            base.Awake();

            _bossHealthBar = GetComponent<UIDocument>();

            _root = _bossHealthBar.rootVisualElement;

            _healthBar = _root.Q<VisualElement>("BossHealthBar");
            _healthBarFill = _root.Q<VisualElement>("BossHealthBarFill");
            _healthBarWhiteFill = _root.Q<VisualElement>("BossHealthBarWhiteFill");

            _healthText = _root.Q<Label>("BossHealthText");
            _bossName = _root.Q<Label>("BossName");
            _bossDescription = _root.Q<Label>("BossDescription");

            _healthBarFill.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            _healthBarWhiteFill.style.width = new StyleLength(new Length(100, LengthUnit.Percent));

            _healthText.text = $"{Health}/{MaxHealth.Value}";
            _bossName.text = BossName;
            _bossDescription.text = BossDescription;

            DeathEvent += HideHealthBar;

            RevealBar();

            InvokeRepeating(nameof(DebugHurt), 5f, 2.5f);
        }

        public override void Damage(DamageSource source) {
            Debug.Log(_healthBarFill.style.width);
            base.Damage(source);
            _healthText.text = $"{Health}/{MaxHealth.Value}";
            _healthBarFill.style.width = new StyleLength(new Length(Health / MaxHealth.Value * 100, LengthUnit.Percent));
            BarDamageEffect();
        }

        public void DebugHurt() {
            Damage(new DamageSource(25, DamageType.COMBAT, 4, this, this));
        }

        // making another renderer class would have been a bit more clean, but i think in this case it's better to do it this way

        private void RevealBar() {
            var tween = new FloatTween {
                duration = 2,
                from = 0,
                to = 80,
                easeType = EaseType.ExpoInOut,
                onUpdate = (_, value) => {
                    _healthBar.style.width = new StyleLength(new Length(value, LengthUnit.Percent));
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
                    _healthText.style.opacity = value;
                    _bossName.style.opacity = value;
                    _bossDescription.style.opacity = value;
                }
            };
            gameObject.AddTween(tween);
        }

        private void BarDamageEffect() {
            var tween = new FloatTween {
                duration = 0.75f,
                from = _healthBarWhiteFill.style.width.value.value,
                to = _healthBarFill.style.width.value.value,
                easeType = EaseType.ExpoInOut,
                onUpdate = (_, value) => {
                    _healthBarWhiteFill.style.width = new StyleLength(new Length(value, LengthUnit.Percent));
                }
            };

            gameObject.AddTween(tween);
        }

        private void HideHealthBar() {
            var tween = new FloatTween {
                duration = 0.25f,
                from = 1,
                to = 0,
                onUpdate = (_, value) => {
                    _healthText.style.opacity = value;
                    _bossName.style.opacity = value;
                    _bossDescription.style.opacity = value;
                }
            };
            gameObject.AddTween(tween);
        }
    }
}