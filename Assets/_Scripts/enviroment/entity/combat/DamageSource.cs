using UnityEngine;

namespace roguelike.enviroment.entity.combat {
    public class DamageSource {
        public float Damage { get; private set; }
        public DamageType type { get; private set; }
        public int DamageTier { get; private set; }
        public Entity Target { get; private set; }

        public float GetDamage() {

            if(type != DamageType.COMBAT) {
                return Mathf.RoundToInt(Damage);
            }

            var defenceTier = Target.GetDefenceTier();

            return 0; // todo: finish, out of time rn
        }
    }

    public enum DamageType {
        COMBAT,
        ENVIROMENT,
        CORRUPTION
    }
}
