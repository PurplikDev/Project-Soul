using io.purplik.ProjectSoul.Entity.Stats;
using io.purplik.ProjectSoul.InventorySystem;
using UnityEngine;
using static io.purplik.ProjectSoul.Entity.LivingEntity;

namespace io.purplik.ProjectSoul.Entity {

public class LivingEntity : MonoBehaviour, IDamagable
{

        [Header("Rendering")]
        public Animator animator;

        [Header("Stats")]
        public EntityStat templar;
        public EntityStat thaumaturge;
        public EntityStat rogue;
        [Space]
        public EntityStat health;
        public EntityStat defence;
        public EntityStat speed;
        [Space]
        public EntityStat damage;

        [Header("<color=#80FF75>Active Stats")]
        public int activeHealth;
        public int maxActiveHealth;

        [Header("Misc Stuff")]
        public EntityState entityState;

        private void Awake()
        {
            maxActiveHealth = Mathf.FloorToInt(health.Value);
            activeHealth = maxActiveHealth;
        }

        public void UpdateMaxHealth() => maxActiveHealth = Mathf.FloorToInt(health.Value);

        public void Damage(int damage, DamageType damageType)
        {
            switch (damageType) {
                case DamageType.TRUE:
                default:
                    activeHealth -= damage;
                    break;
            }

            if (activeHealth <= 0)
            {
                Die();
            }
        }

        public virtual void Attack()
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f);
            if (hit.transform == null) { return; }
            var damagableEntity = hit.transform.gameObject.GetComponent<IDamagable>();
            if (damagableEntity != null)
            {
                damagableEntity.Damage(5 ,DamageType.MELE);
            }
        }


        public enum DamageType
        {
            TRUE,
            MELE,
            MAGIC
        }

        public enum EntityState
        {
            IDLE,
            BLOCKING,
            ATTACKING,
            MOVING
        }

        private void Die()
        {
            speed.baseValue = 0;
            Debug.Log("I'm dead");
            Invoke(nameof(SelfDestruct), 5f);
        }

        private void SelfDestruct()
        {
            Destroy(gameObject);
        }
    }

    internal interface IDamagable
    {
        public void Damage(int damage, DamageType damageType);
    }
}