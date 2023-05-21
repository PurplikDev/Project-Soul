using io.purplik.ProjectSoul.InventorySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace io.purplik.ProjectSoul.SaveSystem
{
    public class ItemSaveManager : MonoBehaviour
    {
        [SerializeField] ItemDatabase itemDatabase;

        private const string InventoryFileName = "Inventory";
        private const string EquipmentFileName = "Equipment";

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void LoadInventory(PlayerInventoryManager player)
        {
            ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFileName);
            if (savedSlots == null) return;
            player.inventory.Clear();

            for (int i = 0; i < savedSlots.SavedSlots.Length; i++)
            {
                ItemSlot itemSlot = player.inventory.itemSlots[i];
                ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

                if (savedSlot == null)
                {
                    itemSlot.item = null;
                    itemSlot.itemAmount = 0;
                }
                else
                {
                    itemSlot.item = itemDatabase.GetItemCopy(savedSlot.ItemID);
                    itemSlot.itemAmount = savedSlot.Amount;
                }
            }
        }

        public void LoadEquipment(PlayerInventoryManager player)
        {
            ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFileName);
            if (savedSlots == null) return;

            foreach (ItemSlotSaveData savedSlot in savedSlots.SavedSlots)
            {
                if (savedSlot == null)
                {
                    continue;
                }

                Item item = itemDatabase.GetItemCopy(savedSlot.ItemID);
                player.inventory.AddItem(item);
                player.Equip((EquipmentItem)item);
            }
        }

        public void SaveInventory(PlayerInventoryManager player)
        {
            SaveItems(player.inventory.itemSlots, InventoryFileName);
        }

        public void SaveEquipment(PlayerInventoryManager player)
        {
            SaveItems(player.equipmentInventory.equipmentSlots, EquipmentFileName);
        }

        private void SaveItems(IList<ItemSlot> itemSlots, string fileName)
        {
            var saveData = new ItemContainerSaveData(itemSlots.Count);

            for (int i = 0; i < saveData.SavedSlots.Length; i++)
            {
                ItemSlot itemSlot = itemSlots[i];

                if (itemSlot.item == null)
                {
                    saveData.SavedSlots[i] = null;
                }
                else
                {
                    saveData.SavedSlots[i] = new ItemSlotSaveData(itemSlot.item.ID, itemSlot.itemAmount);
                }
            }

            ItemSaveIO.SaveItems(saveData, fileName);
        }

        public void DeleteAllSave()
        {
            ItemSaveIO.DeleteItems(InventoryFileName);
            ItemSaveIO.DeleteItems(EquipmentFileName);
        }
    }
}