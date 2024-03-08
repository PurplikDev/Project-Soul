using roguelike.environment.entity.combat;
using Tweens;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.entity {
    public class BossEntity : HostileEntity {
        private UIDocument _bossHealthBar;
        private VisualElement _root, _healthBar, _healthBarFill;
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

            _healthText = _root.Q<Label>("BossHealthText");
            _bossName = _root.Q<Label>("BossName");
            _bossDescription = _root.Q<Label>("BossDescription");

            _healthText.text = $"{Health}/{MaxHealth.Value}";
            _bossName.text = BossName;
            _bossDescription.text = BossDescription;

            DeathEvent += HideHealthBar;

            RevealBar();

            InvokeRepeating(nameof(DebugHurt), 5f, 2.5f);
        }

        public override void Damage(DamageSource source) {
            base.Damage(source);
            _healthText.text = $"{Health}/{MaxHealth.Value}";
            _healthBarFill.style.width = new StyleLength(new Length((Health / MaxHealth.Value) * 100, LengthUnit.Percent));
        }

        public void DebugHurt() {
            DamageSource source = new DamageSource(25, DamageType.COMBAT, 2, this, this);
            Debug.Log($"Damaging for: {source.CalculateDamage()}");
            Damage(source);
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