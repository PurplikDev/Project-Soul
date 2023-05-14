using io.purplik.ProjectSoul.Entity.Stats;
using io.purplik.ProjectSoul.InventorySystem;
using UnityEngine;

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

        [Header("<color=#80FF75>Active Stats")]
        public int activeHealth;
        public int maxActiveHealth;

        [Header("Misc Stuff")]
        public EntityState entityState;

        private void Awake()
        {
            maxActiveHealth = Mathf.FloorToInt(health.Value);
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
                // Drop inventory on death here
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
    }

    public interface IDamagable
    {
        void Damage(int damage, LivingEntity.DamageType damageType);
    } 
}