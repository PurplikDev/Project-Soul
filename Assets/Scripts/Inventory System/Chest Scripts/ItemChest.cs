using io.purplik.ProjectSoul.Entity;
using io.purplik.ProjectSoul.Entity.Player;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem { 
    public class ItemChest : ItemContainer, ITileEntity
    {
        [SerializeField] Animator animator;
        [SerializeField] Transform itemsParent;
        [SerializeField] Item[] startingItems;
        bool isOpen = false;

        public PlayerInventoryManager playerInventory;
        private PlayerEntity playerEntity;

        public void Interact()
        {
            itemsParent.gameObject.SetActive(true);
            isOpen = true;
            playerInventory.OpenItemContainer(this);
        }

        protected override void OnValidate()
        {
            playerEntity = GameObject.Find("Player").GetComponent<PlayerEntity>();
            if (itemsParent != null)
            {
                itemsParent.GetComponentsInChildren(includeInactive: true, result: itemSlots);
            }
        }

        private void Awake()
        {
            itemsParent.gameObject.SetActive(false);
            SetStartingItems();
        }

        private void Update()
        {
            if ((Input.GetKeyDown(PlayerKeybinds.openInventory) || Input.GetKeyDown(PlayerKeybinds.pauseGame)) && isOpen)
            {
                CloseUI();
            }

            playerInventory = playerEntity.GetComponentInChildren<PlayerInventoryManager>();
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponentInParent<PlayerEntity>();
            if(player != null && isOpen)
            {
                CloseUI();
                player.GetComponentInChildren<PlayerUIController>().ToggleAllUI(false);
            }
        }

        public void CloseUI()
        {
            playerInventory.CloseItemContainer(this);
            itemsParent.gameObject.SetActive(false);
            isOpen = false;
            playerInventory = null;
        }

        private void SetStartingItems()
        {
            for (int i = 0; i < startingItems.Length; i++)
            {
                AddItem(startingItems[i].GetCopy());
            }
        }
    }
}