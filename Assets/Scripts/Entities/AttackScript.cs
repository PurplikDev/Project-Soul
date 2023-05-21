using io.purplik.ProjectSoul.Entity;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] LivingEntity living;
    public void AttackAnimation()
    {
        living.Attack();
    }
}
