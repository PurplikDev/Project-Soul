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
            if(Type != DamageType.COMBAT) {
                return Mathf.RoundToInt(Damage);
            }

            var defenceTier = Target.GetDefenceTier();

            float damageToReturn = Damage;

            if(DamageTier > defenceTier) {
                return damageToReturn;
            } else if(DamageTier < defenceTier) {

            } 

            return Damage; // todo: finish, was lazy this time
        }
    }

    public enum DamageType {
        COMBAT,
        ENVIROMENT,
        CORRUPTION
    }
}
