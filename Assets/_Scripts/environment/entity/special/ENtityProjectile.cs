using UnityEngine;

namespace roguelike.environment.entity.special {
    public class EntityProjectile : MonoBehaviour {
        [SerializeField] GameObject projectileImage;

        public Entity owner;
        public float ProjectileSpeed;

        private void Awake() {
            Invoke(nameof(Kill), 7.5f);
        }

        private void Update() {
            transform.position += transform.forward * Time.deltaTime * ProjectileSpeed;
            projectileImage.transform.LookAt(Camera.main.transform);
        }

        private void OnTriggerEnter(Collider other) {
            var entity = other.GetComponent<Entity>();
            if(entity != owner) {
                entity.Damage(new combat.DamageSource(((HostileEntity)owner).AttackDamage, combat.DamageType.COMBAT, ((HostileEntity)owner).DamageTier,entity ,owner));
                Debug.Log(entity.EntityName);
                Destroy(gameObject);
            }
        }

        private void Kill() {
            Destroy(gameObject);
        }
    }
}