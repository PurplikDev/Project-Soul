using io.purplik.ProjectSoul.Entity;
using io.purplik.ProjectSoul.Entity.Player;
using UnityEngine;
using UnityEngine.UI;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        LivingEntity livingEntity;
        [Space]
        [Header("<color=#6F90FF>Inventory")]
        [SerializeField] Inventory inventory;
        [SerializeField] EquipmentInventory equipmentInventory;
        [SerializeField] StatDisplayInventory statDisplayInventory;
        [Space]
        [SerializeField] ItemTooltip itemTooltip;
        [SerializeField] Image dragItem;
        [Space]
        [SerializeField] DropItem dropItem;

        private ItemSlot dragSlot;

        private void OnValidate()
        {
            livingEntity = GetComponentInParent<PlayerEntity>();

            inventory = GetComponentInChildren<Inventory>();
            equipmentInventory = GetComponentInChildren<EquipmentInventory>();
            statDisplayInventory = GetComponentInChildren<StatDisplayInventory>();

            if (itemTooltip == null)
            {
                itemTooltip = FindObjectOfType<ItemTooltip>();
            }
        }

        private void Awake()
        {
            statDisplayInventory.SetStats(livingEntity.templar, livingEntity.thaumaturge, livingEntity.rogue, livingEntity.health, livingEntity.defence, livingEntity.speed);
            statDisplayInventory.UpdateStatValues();

            //OnRightClickEvent
            inventory.OnRightClickEvent += Equip;
            equipmentInventory.OnRightClickEvent += Unequip;

            //OnPointerEnterEvent
            inventory.OnPointerEnterEvent += ShowTooltip;
            equipmentInventory.OnPointerEnterEvent += ShowTooltip;

            //OnPointerExitEvent
            inventory.OnPointerExitEvent += HideTooltip;
            equipmentInventory.OnPointerExitEvent += HideTooltip;

            //OnBeginDragEvent
            inventory.OnBeginDragEvent += BeginDrag;
            equipmentInventory.OnBeginDragEvent += BeginDrag;

            //OnEndDragEvent
            inventory.OnEndDragEvent += EndDrag;
            equipmentInventory.OnEndDragEvent += EndDrag;

            //OnDragEvent
            inventory.OnDragEvent += Drag;
            equipmentInventory.OnDragEvent += Drag;

            //OnDropEvent
            inventory.OnDropEvent += Drop;
            equipmentInventory.OnDropEvent += Drop;
            //dropItem.OnDropEvent += DropItemOnGround;

        }
        

        private void Equip(ItemSlot slot)
        {
            EquipmentItem item = slot.item as EquipmentItem;
            if (item != null)
            {
                Equip(item);
            }
        }

        private void Unequip(ItemSlot slot)
        {
            EquipmentItem item = slot.item as EquipmentItem;
            if (item != null)
            {
                Unequip(item);
            }
        }

        private void ShowTooltip(ItemSlot slot)
        {
            EquipmentItem item = slot.item as EquipmentItem;
            if (item != null)
            {
                itemTooltip.ShowTooltip(item);
            }
        }

        private void HideTooltip(ItemSlot slot)
        {
            itemTooltip.HideTooltip();
        }

        private void BeginDrag(ItemSlot slot)
        {
            if(slot.item != null)
            {
                dragSlot = slot;
                dragItem.sprite = slot.item.icon;
                dragItem.transform.position = Input.mousePosition;
                dragItem.enabled = true;
            }
        }

        private void EndDrag(ItemSlot slot)
        {
            dragSlot = null;
            dragItem.enabled = false;
        }

        private void Drag(ItemSlot slot)
        {
            if(dragItem.enabled)
            {
                dragItem.transform.position = Input.mousePosition;
            }
        }

        private void Drop(ItemSlot dropSlot)
        {
            if (dragSlot == null) return;


            if(dropSlot.CanAddStack(dragSlot.item))
            {
                AddStacks(dropSlot);
            }


            if (dropSlot.IsValidItem(dragSlot.item) && dragSlot.IsValidItem(dropSlot.item))
            {
                SwapItems(dropSlot);
            }
        }

        private void DropItemOnGround()
        {
            if(dragItem == null)
            {
                return;
            }
        }

        private void SwapItems(ItemSlot dropSlot)
        {
            EquipmentItem __dragItem = dragSlot.item as EquipmentItem;
            EquipmentItem dropItem = dropSlot.item as EquipmentItem;

            if (dragSlot is EquipmentSlot)
            {
                if (__dragItem != null) __dragItem.Unequip(livingEntity);
                if (dropItem != null) dropItem.Equip(livingEntity);
            }

            if (dropSlot is EquipmentSlot)
            {
                if (__dragItem != null) __dragItem.Equip(livingEntity);
                if (dropItem != null) dropItem.Unequip(livingEntity);
            }

            statDisplayInventory.UpdateStatValues();

            Item _dragItem = dragSlot.item;
            int draggedItemAmount = dragSlot.itemAmount;

            dragSlot.item = dropSlot.item;
            dragSlot.itemAmount = dropSlot.itemAmount;

            dropSlot.item = _dragItem;
            dropSlot.itemAmount = draggedItemAmount;
        }

        private void AddStacks(ItemSlot dropSlot)
        {
            int addableStacks = dropSlot.item.maxStackSize - dropSlot.itemAmount;
            int stackToAdd = Mathf.Min(addableStacks, dragSlot.itemAmount);

            dragSlot.itemAmount += stackToAdd;
            dropSlot.itemAmount -= stackToAdd;
        }

        public void Equip(EquipmentItem item)
        {
            if (inventory.RemoveItem(item))
            {
                EquipmentItem previousItem;
                if (equipmentInventory.AddItem(item, out previousItem))
                {
                    if (previousItem != null)
                    {
                        inventory.AddItem(previousItem);
                        previousItem.Unequip(livingEntity);
                    }
                    item.Equip(livingEntity);
                    statDisplayInventory.UpdateStatValues();
                    livingEntity.UpdateMaxHealth();
                }
                else
                {
                    inventory.AddItem(item);
                }
            }
        }
        public void Unequip(EquipmentItem item)
        {
            if (!inventory.CanAddItem(item) && equipmentInventory.RemoveItem(item))
            {
                item.Unequip(livingEntity);
                statDisplayInventory.UpdateStatValues();
                livingEntity.UpdateMaxHealth();
                inventory.AddItem(item);
            }
        }

        private ItemContainer openContainer;

        private void TransferToContainer(ItemSlot itemSlot)
        {
            Item item = itemSlot.item;
            if (item != null && openContainer.CanAddItem(item))
            {
                inventory.RemoveItem(item);
                openContainer.AddItem(item);
            }
        }

        private void TransferToInventory(ItemSlot itemSlot)
        {
            Item item = itemSlot.item;
            if (item != null && openContainer.CanAddItem(item))
            {
                openContainer.RemoveItem(item);
                inventory.AddItem(item);
            }
        }

        public void OpenItemContainer(ItemContainer itemContainer)
        {
            openContainer = itemContainer;

            inventory.OnRightClickEvent -= Equip;

            inventory.OnRightClickEvent += TransferToContainer;
            itemContainer.OnRightClickEvent += TransferToInventory;

            itemContainer.OnPointerEnterEvent += ShowTooltip;
            itemContainer.OnPointerExitEvent += HideTooltip;
            itemContainer.OnBeginDragEvent += BeginDrag;
            itemContainer.OnEndDragEvent += EndDrag;
            itemContainer.OnDragEvent += Drag;
            itemContainer.OnDropEvent += Drop;
        }

        public void CloseItemContainer(ItemContainer itemContainer)
        {
            openContainer = null;

            inventory.OnRightClickEvent += Equip;

            inventory.OnRightClickEvent -= TransferToContainer;
            itemContainer.OnRightClickEvent -= TransferToInventory;

            itemContainer.OnPointerEnterEvent -= ShowTooltip;
            itemContainer.OnPointerExitEvent -= HideTooltip;
            itemContainer.OnBeginDragEvent -= BeginDrag;
            itemContainer.OnEndDragEvent -= EndDrag;
            itemContainer.OnDragEvent -= Drag;
            itemContainer.OnDropEvent -= Drop;
        }
    }
}
