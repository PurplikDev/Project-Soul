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
        }
    }
}