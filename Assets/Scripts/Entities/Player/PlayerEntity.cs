using io.purplik.ProjectSoul.Entity.Player;
using io.purplik.ProjectSoul.InventorySystem;
using UnityEngine;

namespace io.purplik.ProjectSoul.Entity { 
    public class PlayerEntity : LivingEntity
    {
        private void Update()
        {
            if(Input.GetKeyDown(PlayerKeybinds.primaryAction))
            {
                animator.SetTrigger("Attack");
            }
        }

        public override void Attack()
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f);
            if (hit.transform == null) { return; }
            var damagableEntity = hit.transform.gameObject.GetComponent<IDamagable>();
            if (damagableEntity != null)
            {
                damagableEntity.Damage((int) damage.Value, DamageType.MELE);
                Debug.Log(damage.Value);
            }
        }
    }
}