using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Stats", menuName = "Entity/Entity Stats")]
public class EntityStats : ScriptableObject
{
    public int health;
    public int defence;
    public int damage;
    public int speed;
}
