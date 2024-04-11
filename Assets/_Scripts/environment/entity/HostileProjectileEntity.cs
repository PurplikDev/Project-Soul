using roguelike.environment.entity.special;
using roguelike.system.manager;
using UnityEngine;

namespace roguelike.environment.entity {

    public class HostileProjectileEntity : HostileEntity {
        [Space]
        public GameObject Projectile;

        internal override void Attack() {
            var projectile = Instantiate(Projectile, new Vector3(transform.position.x, GameManager.Player.transform.position.y, transform.position.z), Rotation);
            var proj_ent = projectile.GetComponent<EntityProjectile>();
            proj_ent.owner = this;
        }
    }
}