using Newtonsoft.Json;
using roguelike.core.eventsystem;
using roguelike.core.utils.gamedata;
using roguelike.environment.entity;
using roguelike.environment.entity.player;
using roguelike.environment.entity.statsystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace roguelike.core.item {
    public class Inventory {

        public static readonly int InventorySize = 20; // this reffers to the size of the generic item storage, not the amount of all the slots
        public static readonly int EquipmentSize = 6;
        public static readonly int TrinketSize = 8;

        public List<ItemStack> Items = new List<ItemStack>(34);

        public Entity Entity;

        public Inventory(Entity entity) {
            Entity = entity;
            FillAllSlots();
        }

        public Inventory(Entity entity, InventoryData data) {
            Entity = entity;
            LoadItemsFromSave(data);
        }

        // todo: proper implementation of this method by loading content from a safe file or filling it with air
        private void FillAllSlots() {
            for (int i = 0; i < InventorySize; i++) { Items.Add(ItemStack.EMPTY); }
            for (int i = 0; i < EquipmentSize; i++) { Items.Add(ItemStack.EMPTY); }
            for (int i = 0; i < TrinketSize; i++) { Items.Add(ItemStack.EMPTY); }
        }

        public void LoadItemsFromSave(InventoryData data) {
            for(int i = 0; i < data.Items.Count; i++) {
                Items.Add(new ItemStack(data.Items[i]));
            }
        }

        public void UpdateItemStack(ItemStack itemStack, int index) {
            ItemStack oldStack = Items[index];
            Items[index] = itemStack;

            // The purpose of this check is to update stats only when necesseary.
            // There is no reason to update stats when an item in placed in a storage slot
            if(index >= InventorySize) {
                if(itemStack.Item is EquipmentItem item) {
                    foreach(StatModifier statModifier in item.StatModifiers) {
                        Entity.StatByType[statModifier.StatType].AddModifier(statModifier);
                    }
                } else if(itemStack.IsEmpty() && oldStack.Item is EquipmentItem oldEquipment) {
                    foreach(StatModifier statModifier in oldEquipment.StatModifiers) {
                        Entity.StatByType[statModifier.StatType].RemoveModifier(statModifier);
                    }
                }

                Events.PlayerHeathUpdateEvent.Invoke(new PlayerHealthUpdateEvent((Player)Entity));
            }
        }
    }

    public class InventoryData {
        public List<ItemStackData> Items = new List<ItemStackData>();
        public InventoryData(Inventory inventory) {
            foreach(ItemStack stack in  inventory.Items) {
                Items.Add(new ItemStackData(stack));
            }
        }

        [JsonConstructor]
        public InventoryData(params ItemStackData[] items) {
            Items = items.ToList();
        }

        /// <summary>
        /// This constructor creates and empty inventory!
        /// </summary>
        public InventoryData() {
            for (int i = 0; i < Inventory.InventorySize; i++) { Items.Add(ItemStackData.EMPTY); }
            for (int i = 0; i < Inventory.EquipmentSize; i++) { Items.Add(ItemStackData.EMPTY); }
            for (int i = 0; i < Inventory.TrinketSize; i++) { Items.Add(ItemStackData.EMPTY); }
        }
    }
}
