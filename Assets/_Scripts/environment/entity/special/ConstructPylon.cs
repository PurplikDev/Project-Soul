using roguelike.environment.entity.combat;
using UnityEngine;

namespace roguelike.environment.entity {
    public class ConstructPylon : HostileEntity {

        public HostileEntity AncientConstructCore;

        protected override void Awake() {
            base.Awake();
            DeathEvent += HurtCore;
        }

        public override void Damage(DamageSource source) {
            if(source.Attacker.EntityName != "Ancient Construct") {
                return;
            }
            base.Damage(source);
        }

        private void HurtCore() {
            float damageToDo = AncientConstructCore.MaxHealth.Value / 3f;
            AncientConstructCore.SetHealth(AncientConstructCore.Health - damageToDo);
        }
    }
}