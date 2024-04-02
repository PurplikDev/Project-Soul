using UnityEngine;

namespace roguelike.environment.entity {

    public class HostileProjectileEntity : HostileEntity {

        public GameObject Projectile;

        internal override void Attack() {
            Instantiate(Projectile, transform.position, Rotation);
        }
    }
}