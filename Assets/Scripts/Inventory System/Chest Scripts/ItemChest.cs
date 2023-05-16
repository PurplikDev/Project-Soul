using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem { 
    public class ItemChest : ItemContainer
    {
        [SerializeField] Transform itemsParent;

        protected override void OnValidate()
        {

            if (itemsParent != null)
            {
                itemsParent.GetComponentsInChildren(includeInactive: true, result: itemSlots);
            }
        }

        private void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckCollision(other.gameObject, true);
        }

        private void OnTriggerExit(Collider other)
        {
            CheckCollision(other.gameObject, false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CheckCollision(collision.gameObject, true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            CheckCollision(collision.gameObject, false);
        }

        private void CheckCollision(GameObject gameObject, bool state)
        {
        }
    }
}