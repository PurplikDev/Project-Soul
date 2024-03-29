using roguelike.core.item;
using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.environment.entity.combat {
    public class DamageSource {
        public float Damage { get; private set; }
        public DamageType Type { get; private set; }
        public int DamageTier { get; private set; }
        public Entity Target { get; private set; }
        public Entity Attacker { get; private set; }

        public DamageSource(float damage, DamageType damageType, int damageTier, Entity target, Entity attacker) {
            Damage = damage;
            Type = damageType;
            DamageTier = damageTier;
            Target = target;
            Attacker = attacker;
        }

        public float CalculateDamage() {
            if(Target.Immortal) { return 0; }

            float angle = Vector3.Angle(Target.LookDirection, Attacker.LookDirection);

            if(angle >= 165 && angle <= 195 && Target.IsBlocking) {
                var player = (Player)Target;
                var shield = (Shield)player?.ItemInOffHand;
                if(shield.WeaponTier >= DamageTier) {
                    shield.Blocked(player);
                    return 0;
                }
            }

            if(Type != DamageType.COMBAT) {
                return Mathf.RoundToInt(Damage);
            }

            double damageTier = DamageTier;
            double defenceTier = Target.GetDefenceTier();

            if(defenceTier == 0) {
                return Mathf.FloorToInt(Damage * ((float)damageTier + 0.5f));
            }

            return Mathf.FloorToInt(Damage * (float)(damageTier / defenceTier));
        }
    }

    public enum DamageType {
        COMBAT,
        ENVIROMENT,
        CORRUPTION
    }
}
