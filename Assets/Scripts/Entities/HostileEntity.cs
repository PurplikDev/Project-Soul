using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.purplik.ProjectSoul.Entity
{
    public class HostileEntity : LivingEntity
    {
        protected bool canAttack = true;
        protected virtual void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerEntity>();
            if(player != null)
            {
                if(canAttack)
                {
                    player.Damage(5);
                    canAttack = false;
                    StartCoroutine(AttackCooldown(player));
                }
            }
        }

        IEnumerator AttackCooldown(PlayerEntity player)
        {
            yield return new WaitForSecondsRealtime(2);
            canAttack = true;
        }

        protected override void Die()
        {
            Destroy(gameObject);
        }
    }
}