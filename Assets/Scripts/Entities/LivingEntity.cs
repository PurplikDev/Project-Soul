using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
    [Header("Entity Stats")]
    public EntityStats entityStats;
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
        health = entityStats.health;
        defence = entityStats.defence;
        damage = entityStats.damage;
        speed = entityStats.speed;

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

public interface IDamagable
{
    void Damage(int damage, LivingEntity.DamageType damageType);
}