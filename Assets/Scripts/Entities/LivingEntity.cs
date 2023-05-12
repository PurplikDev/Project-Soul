using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.purplik.ProjectSoul.Entity {


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
    {
        switch (damageType) {
            case DamageType.TRUE:
            default:
                health -= damage;
                break;
        }

        if(health <= 0)
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
}
}