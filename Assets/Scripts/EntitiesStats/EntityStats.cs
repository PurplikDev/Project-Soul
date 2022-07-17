using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int baseHealth;
    public int maxHealth;
    public int health { get; private set; }

    [Min(1)]
    public EntityStat entityLevel;

    public EntityStat attackDamage { get; private set; }
    public EntityStat reach;

    void Awake() {
        maxHealth = baseHealth + (entityLevel.GetValue() * 10);
        health = maxHealth;
    }

    public void Damage(int damageAmount, string damageType) {
        switch(damageType) {
            case "true": health -= damageAmount; break;
        }
    }

    void Update() {
       if(health <= 0 && !gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        } 
    }
}
