using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using io.purplik.ProjectSoul.Entity.Stats;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class Character : MonoBehaviour
    {
        public EntityStat templar;
        public EntityStat thaumaturge;
        public EntityStat rogue;

        public EntityStat health;
        public EntityStat defence;
        public EntityStat speed;

        [SerializeField] Inventory inventory;
        [SerializeField] EquipmentInventory equipmentInventory;
        [SerializeField] StatDisplayInventory statDisplayInventory;

        private void Awake()
        {
            statDisplayInventory.SetStats(templar, thaumaturge, rogue, health, defence, speed);
            statDisplayInventory.UpdateStatValues();

            inventory.OnRightClickedEvent += EquipFromInventory;
            equipmentInventory.OnRightClickedEvent += UnequipFromEquipmentInventory;
        }

        private void EquipFromInventory(Item item)
        {
            if(item is EquipmentItem)
            {
                Equip((EquipmentItem)item);
            }
        }

        private void UnequipFromEquipmentInventory(Item item)
        {
            if (item is EquipmentItem)
            {
                Unequip((EquipmentItem)item);
            }
        }

        public void Equip(EquipmentItem item)
        {
            if(inventory.RemoveItem(item))
            {
                EquipmentItem previousItem;
                if(equipmentInventory.AddItem(item, out previousItem))
                {
                    if(previousItem != null)
                    {
                        inventory.AddItem(previousItem);
                        previousItem.Unequip(this);
                        statDisplayInventory.UpdateStatValues();
                    }
                    item.Equip(this);
                    statDisplayInventory.UpdateStatValues();
                } else
                {
                    inventory.AddItem(item);
                }
            }
        }

        public void Unequip(EquipmentItem item)
        {
            if(!inventory.IsFull() && equipmentInventory.RemoveItem(item))
            {
                item.Unequip(this);
                statDisplayInventory.UpdateStatValues();
                inventory.AddItem(item);
            }
        }
    }
}