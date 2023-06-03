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
        public AudioSource source;
        [Space]
        public AudioClip hitSound;
        public AudioClip attackSound;

        private void Start()
        {
            UpdateMaxHealth();
            activeHealth = maxActiveHealth;
        }

        public void UpdateMaxHealth() => maxActiveHealth = Mathf.FloorToInt(health.Value);

        public virtual void Damage(int damage, DamageType damageType = DamageType.TRUE)
        {
            source.PlayOneShot(hitSound);
            switch (damageType) {
                case DamageType.MELE:
                    if((damage - (int)defence.Value) < 10)
                    {
                        activeHealth -= 10;
                    } else
                    {
                        activeHealth -= damage - (int)defence.Value;
                    }
                    break;

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
            source.PlayOneShot(attackSound);
            if (hit.transform == null) { return; }
            var entity = hit.transform.gameObject.GetComponent<LivingEntity>();
            if (entity != null)
            {
                entity.Damage((int)damage.Value, DamageType.MELE);
            }
        }


        public enum DamageType
        {
            TRUE,
            MELE
        }

        public enum EntityState
        {
            IDLE,
            BLOCKING,
            ATTACKING,
            MOVING
        }

        protected virtual void Die()
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