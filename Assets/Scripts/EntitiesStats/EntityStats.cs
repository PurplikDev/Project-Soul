using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int baseHealth = 100;
    public int maxHealth;
    public int health { get; private set; }
    public bool alive;

    public EntityStat enemyLevel;

    public EntityStat attackDamage;
    public EntityStat reach;

    void Awake() {
        maxHealth = baseHealth * (enemyLevel.GetValue() * 5);

        health = baseHealth;
    }

    public void Damage(int damageAmount, string damageType) {

        switch(damageType) {
            case "true": health -= damageAmount; break;
        }

    }

    void Update() {
       if(health <= 0)
        {
            Destroy(gameObject);
        } 
    }
}
