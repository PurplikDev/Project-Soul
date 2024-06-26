using Newtonsoft.Json;
using roguelike.core.eventsystem;
using roguelike.core.utils;
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
                UpdateItemStack(Items[i], i); // works??? it's ugly, but works
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

                Entity.MaxHealthUpdate.Invoke();


                // this is the part of the project where i just want to have this over with and start writing dogshit code like this
                // yes, i am not proud of this and yes, i am sorry for writing it like this
                if(Entity is Player player) {
                    if (index == 24) {
                        player.MainHandSprite.sprite = itemStack.Item.Icon;
                    } else if (index == 25) {
                        player.OffHandSprite.sprite = itemStack.Item.Icon;
                    }

                    player.Defence.RemoveModifier(GlobalStaticValues.TEMPLAR_BONUS_STAT);
                    player.Speed.RemoveModifier(GlobalStaticValues.ROGUE_BONUS_STAT);
                    player.MaxHealth.RemoveModifier(GlobalStaticValues.THAUMATURGE_BONUS_STAT);

                    float classValue = player.Templar.Value;
                    StatType playerClass = StatType.TEMPLAR;

                    if (player.Thaumaturge.Value > classValue) {
                        classValue = player.Thaumaturge.Value;
                        playerClass = StatType.THAUMATURGE;
                    }

                    if (player.Rogue.Value > classValue) {
                        classValue = player.Rogue.Value;
                        playerClass = StatType.ROGUE;
                    }

                    if (classValue >= 5) {
                        switch (playerClass) {
                            case StatType.TEMPLAR:
                                player.Defence.AddModifier(GlobalStaticValues.TEMPLAR_BONUS_STAT);
                                break;
                            case StatType.ROGUE:
                                player.Speed.AddModifier(GlobalStaticValues.ROGUE_BONUS_STAT);
                                break;
                            case StatType.THAUMATURGE:
                                player.MaxHealth.AddModifier(GlobalStaticValues.THAUMATURGE_BONUS_STAT);
                                break;
                        }
                    }
                }
            }
        }

        public bool HasItem(Item item) {
            foreach(var stack in Items) {
                if(stack.Item == item) {
                    return true;
                }
            }
            return false;
        }

        public void AddItem(ItemStack itemStack) {
            for(int i = 0; i < InventorySize; i++) {
                if(Items[i].IsEmpty()) {
                    Items[i] = itemStack; return;
                }
            }
        }

        public void RemoveItem(Item item) {
            for(int i = 0; i < Items.Count; i++) {
                if (Items[i].Item == item) {
                    Items[i] = ItemStack.EMPTY; return;
                }
            }
        }

        public bool HasSpace() {
            foreach (var stack in Items) {
                if (stack.IsEmpty()) {
                    return true;
                }
            }
            return false;
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
            for (int i = 0; i < Inventory.InventorySize - 7; i++) { Items.Add(ItemStackData.EMPTY); }

            Items.Add(new ItemStackData(new ItemStack(ItemManager.GetItemByID("trainer_sword"))));
            Items.Add(new ItemStackData(new ItemStack(ItemManager.GetItemByID("trainer_shield"))));
            Items.Add(new ItemStackData(new ItemStack(ItemManager.GetItemByID("trainer_helmet"))));
            Items.Add(new ItemStackData(new ItemStack(ItemManager.GetItemByID("trainer_tunic"))));
            Items.Add(new ItemStackData(new ItemStack(ItemManager.GetItemByID("trainer_leggings"))));
            Items.Add(new ItemStackData(new ItemStack(ItemManager.GetItemByID("trainer_boots"))));
            Items.Add(new ItemStackData(new ItemStack(ItemManager.GetItemByID("trainer_talisman"))));

            for (int i = 0; i < Inventory.EquipmentSize; i++) { Items.Add(ItemStackData.EMPTY); }
            for (int i = 0; i < Inventory.TrinketSize; i++) { Items.Add(ItemStackData.EMPTY); }
        }
    }
}
