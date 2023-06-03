using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace io.purplik.ProjectSoul.Entity {
    public class BossEntity : HostileEntity
    {
        public GameObject wall;
        public PlayerEntity player;
        protected void OnTriggerStay(Collider other)
        {
            var playerE = other.GetComponent<PlayerEntity>();
            if (playerE != null)
            {
                player = playerE;
                if (canAttack)
                {
                    animator.SetTrigger("Stomp");
                    canAttack = false;
                    StartCoroutine(AttackCooldown());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var playerE = other.GetComponent<PlayerEntity>();
            if (playerE != null)
            {
                player = null;
            }
        }

        IEnumerator AttackCooldown()
        {
            yield return new WaitForSecondsRealtime(7.5f);
            canAttack = true;
        }

        protected override void Die()
        {
            Destroy(wall);
            Destroy(gameObject);
        }
    }
}