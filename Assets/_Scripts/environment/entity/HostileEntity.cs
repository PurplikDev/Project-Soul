using roguelike.environment.entity.combat;
using roguelike.environment.entity.player;
using roguelike.environment.entity.statsystem;
using UnityEngine;

namespace roguelike.environment.entity {
    public class HostileEntity : Entity {
        protected float entityWidth;
        [Space]
        public Stat AttackSpeed = new Stat(1.5f);
        public Stat AttackCooldown = new Stat(0.5f);
        public Stat AttackRange = new Stat(0.5f);
        [Space]
        public Transform EntityAim;

        protected override void Awake() {
            entityWidth = GetComponent<CharacterController>().radius;
            base.Awake();
        }

        protected void Update() {
            if(IsDead) { return; }
            EntityAim.rotation = Rotation;
        }

        internal virtual void Attack() {
            // spawn a sphere in front of the entity
            var colliders = Physics.OverlapSphere(Position, AttackRange.Value + 3, LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);

            foreach(var collider in colliders) {
                var player = collider.GetComponent<Player>();

                player?.Damage(new DamageSource(1f, DamageType.COMBAT, 1, player, this));
            }
        }
    }
}