using io.purplik.ProjectSoul.Entity.Player;
using io.purplik.ProjectSoul.InventorySystem;
using io.purplik.ProjectSoul.SaveSystem;
using System.Collections;
using UnityEngine;

namespace io.purplik.ProjectSoul.Entity { 
    public class PlayerEntity : LivingEntity
    {
        [SerializeField] Transform deathScreen;
        private bool canAttack = true;
        [SerializeField] HealthBar healthBar;

        private void Update()
        {
            if(Input.GetKeyDown(PlayerKeybinds.primaryAction))
            {
                if(canAttack)
                {
                    Attack();
                    animator.Play("Attack");
                    canAttack = false;
                    StartCoroutine(AttackCooldown());
                }
            }
            
            healthBar.SetHealth(activeHealth);
        }

        IEnumerator AttackCooldown()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            canAttack = true;
        }

        protected override void Die()
        {
            //animator.Play("Death");
            GetComponent<PlayerMovement>().lockMovement = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Camera.main.GetComponent<PlayerCamera>().lockRotation = true;

            GetComponentInChildren<PlayerInventoryManager>().equipmentInventory.Clear();
            GetComponentInChildren<PlayerInventoryManager>().inventory.Clear();
            deathScreen.gameObject.SetActive(true);
        }
    }
}