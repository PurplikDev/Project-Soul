using io.purplik.ProjectSoul.Entity.Stats;
using io.purplik.ProjectSoul.InventorySystem;
using UnityEngine;

namespace io.purplik.ProjectSoul.Entity {
<<<<<<< HEAD
    public class LivingEntity : MonoBehaviour, IDamagable
=======


public class LivingEntity : MonoBehaviour, IDamagable
{
    [Header("Entity Stats")]
    public int health;
    public int defence;
    public int damage;
    public int speed;
    public int visionRange;
    
    //[Header("Inventory")]
    //public Inventory entityInventory;

    [Header("Rendering")]
    //public GameObject model;
    public Animator animator;

    private void Awake()
    { 

        //Instantiate(model, transform.position - new Vector3(0,1,0), transform.rotation, transform);
    }

    public void Damage(int damage, DamageType damageType)
>>>>>>> 4014e07cb0394d12341f754bd46a60b23f109116
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
<<<<<<< HEAD
        void Damage(int damage, LivingEntity.DamageType damageType);
    } 
=======
        TRUE,
        MELE,
        MAGIC
    }
}
>>>>>>> 4014e07cb0394d12341f754bd46a60b23f109116
}