using io.purplik.ProjectSoul.Entity;
using io.purplik.ProjectSoul.Entity.Player;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.Progress;

namespace io.purplik.ProjectSoul.InventorySystem { 
    public class ItemChest : ItemContainer, ITileEntity
    {
        [SerializeField] Animator animator;
        [Space]
        [SerializeField] Transform itemsParent;
        bool isOpen = false;

        public PlayerInventoryManager playerInventory;
        [Space]
        private PlayerEntity playerEntity;

        public void Interact()
        {
            itemsParent.gameObject.SetActive(true);
            isOpen = true;
            playerInventory.OpenItemContainer(this);
        }

        protected override void OnValidate()
        {
            if (itemsParent != null)
                itemsParent.GetComponentsInChildren(includeInactive: true, result: itemSlots);
        }

        protected override void Awake()
        {
            base.Awake();
            itemsParent.gameObject.SetActive(false);
        }

        private void Update()
        {
            if ((Input.GetKeyDown(PlayerKeybinds.openInventory) || Input.GetKeyDown(PlayerKeybinds.pauseGame)) && isOpen)
            {
                CloseUI();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerEntity>();
            if(player != null)
            {
                playerEntity = player;
                playerInventory = playerEntity.GetComponentInChildren<PlayerInventoryManager>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(playerEntity != null && isOpen)
            {
                CloseUI();
                playerEntity.GetComponentInChildren<PlayerUIController>().ToggleAllUI(false);
            }
        }

        public void CloseUI()
        {
            playerInventory.CloseItemContainer(this);
            itemsParent.gameObject.SetActive(false);
            isOpen = false;
            playerInventory = null;
        }
    }
}