using roguelike.environment.entity.combat;
using roguelike.environment.ui.hud;
using Tweens;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.entity {
    public class BossEntity : HostileEntity {
        [Header("Boss Information", order = -1)]
        public string BossName;
        public string BossDescription;

        protected override void Awake() {
            base.Awake();
            
            var renderer = GetComponentInChildren<HealthBarRenderer>();
            renderer.InitiateRenderer();
            renderer.SetTarget(this);
        }
    }
}