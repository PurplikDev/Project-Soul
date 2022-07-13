using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int health { get; private set; }

    public EntityStat attackDamage;
    public EntityStat reach;

    void Awake()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Damage(5);
        }
    }

    public void Damage(int damage)
    {

        health -= damage;

        Debug.Log(transform.name + " got damaged for " + damage);


    }

}
