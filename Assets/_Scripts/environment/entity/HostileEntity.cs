using roguelike.environment.entity.combat;
using roguelike.environment.entity.player;
using roguelike.environment.entity.statsystem;
using roguelike.environment.ui.hud;
using UnityEngine;

namespace roguelike.environment.entity {
    public class HostileEntity : Entity {
        protected float entityWidth;
        [Space(order = 4)]
        public Stat AttackSpeed = new Stat(1.5f);
        public Stat AttackCooldown = new Stat(0.5f);
        public Stat AttackRange = new Stat(0.5f);
        [Space(order = 2)]
        public Transform EntityAim;

        public float AttackDamage;
        public int DamageTier;
        [Space]
        public bool HasHealthBar = false;

        protected override void Awake() {
            entityWidth = GetComponent<CharacterController>().radius;
            base.Awake();
            Health = MaxHealth.Value;

            if(HasHealthBar) {
                var renderer = GetComponentInChildren<HealthBarRenderer>();
                renderer.SetTarget(this);
                renderer.InitiateRenderer();
            }
        }

        protected void Update() {
            if(IsDead) { return; }
            EntityAim.rotation = Rotation;
        }

        internal virtual void Attack() {
            AttackEvent.Invoke();
            // spawn a sphere in front of the entity
            var colliders = Physics.OverlapSphere(Position, AttackRange.Value + 3, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);

            foreach(var collider in colliders) {
                var player = collider.GetComponent<Player>();

                player?.Damage(new DamageSource(AttackDamage, DamageType.COMBAT, DamageTier, player, this));
            }
        }
    }
}