using System.Linq;
using roguelike.core.item;
using roguelike.environment.entity.statsystem;
using roguelike.rendering.ui.slot;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class InventoryRenderer : ContainerRenderer {
        protected VisualElement equipmentRoot, trinketRoot, itemTooltip, character;

        protected Label tooltipName, tooltipDescription, classLabel;

        public InventoryRenderer(Inventory entityInventory, UIDocument inventoryUI) : base(entityInventory, inventoryUI) {
            root = inventoryUI.rootVisualElement;
            
            equipmentRoot = root.Q<VisualElement>("EquipmentSlotContainer");
            trinketRoot = root.Q<VisualElement>("TrinketSlotContainer");

            itemTooltip = root.Q<VisualElement>("ItemTooltip");

            tooltipName = itemTooltip.Q<Label>("ItemTooltipName");
            tooltipDescription = itemTooltip.Q<Label>("ItemTooltipDescription");

            character = root.Q<VisualElement>("CharacterBackground");

            classLabel = root.Q<Label>("PlayerClass");

            TranslationManager.TranslateHeader(root.Q<Label>("InventoryHeader"));
            TranslationManager.TranslateHeader(root.Q<Label>("CharacterHeader"));
            TranslationManager.TranslateHeader(root.Q<Label>("EquipmentHeader"));
            TranslationManager.TranslateHeader(root.Q<Label>("TrinketsHeader"));

            

            RegisterEquipmentSlots();
            RegisterTrinketSlots();

            foreach(ItemSlot slot in itemSlots) {
                slot.RegisterCallback<PointerOverEvent>(ShowTooltip);
                slot.RegisterCallback<PointerOutEvent>(HideTooltip);
            }
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            inventory.UpdateItemStack(clickedSlot.SlotStack, clickedSlot.SlotIndex);
        }

        public override void ClickSlot(Vector2 position, ItemSlot originalSlot, int mouseButton) {

            if (mouseButton == 4 && originalSlot.SlotStack.Item is UseItem useItem) {
                useItem.Apply(inventory.Entity);
                originalSlot.SlotStack.DecreaseStackSize(1);
                if(originalSlot.SlotStack.StackSize <= 0) {
                    originalSlot.SetStack(ItemStack.EMPTY);                    
                }
                originalSlot.UpdateSlotEvent.Invoke();
            }

            SelectClass();

            base.ClickSlot(position, originalSlot, mouseButton);
        }

        private void SelectClass() {
            float classValue = inventory.Entity.Templar.Value;
            StatType playerClass = StatType.TEMPLAR;

            if(inventory.Entity.Thaumaturge.Value > classValue) {
                classValue = inventory.Entity.Thaumaturge.Value;
                playerClass = StatType.THAUMATURGE;
            }

            if (inventory.Entity.Rogue.Value > classValue) {
                classValue = inventory.Entity.Rogue.Value;
                playerClass = StatType.ROGUE;
            }

            if(classValue > 0) {
                classLabel.text = $"{playerClass.ToString().ToUpper()} - {classValue}";
            } else {
                classLabel.text = "";
            }
        }



        // SLOT REGISTRATION METHODS

        private void RegisterEquipmentSlots() {
            foreach(EquipmentSlot equipmentSlot in equipmentRoot.Children().ToList()) {
                equipmentSlot.SlotIndex = (int)equipmentSlot.SlotEquipmentType;
                equipmentSlot.ForceSetStack(inventory.Items[equipmentSlot.SlotIndex]);
                equipmentSlot.Renderer = this;
                itemSlots.Add(equipmentSlot);
                equipmentSlot.UpdateSlotEvent.Invoke();
            }
        }

        private void RegisterTrinketSlots() {
            int index = 0;
            foreach (EquipmentSlot equipmentSlot in trinketRoot.Children().ToList()) {
                equipmentSlot.SlotIndex = (int)equipmentSlot.SlotEquipmentType + index;
                equipmentSlot.SetStack(inventory.Items[equipmentSlot.SlotIndex]);
                equipmentSlot.Renderer = this;
                itemSlots.Add(equipmentSlot);
                equipmentSlot.UpdateSlotEvent.Invoke();
                index++; // yes i know a for loop this exact thing, but using that would make this a bit more stupider
            }
        }



        private void ShowTooltip(PointerOverEvent evt) {
            if(evt.currentTarget is ItemSlot slot && !slot.SlotStack.IsEmpty()) {
                tooltipName.text = slot.SlotStack.Item.Name;
                tooltipDescription.text = slot.SlotStack.Item.Description;

                if (slot.SlotStack.Item is EquipmentItem equipment) {

                    if (equipment is WeaponItem weapon) {
                        tooltipDescription.text += $"\n DAMAGE: {weapon.Damage}  TIER: {weapon.WeaponTier}  DURATION: {weapon.SwingSpeed + weapon.AttackCooldown}";
                    } else if (equipment is Shield shield) {
                        tooltipDescription.text += $"\n BLOCKS: {shield.MaxBlockAmount}  TIER: {shield.WeaponTier}";
                    }
                    tooltipDescription.text += $"\n";
                    foreach(var statModifier in equipment.StatModifiers) {
                        tooltipDescription.text += $" {statModifier.ToString()} ";
                    }

                } else if(slot.SlotStack.Item is UseItem) {
                    tooltipDescription.text += $" \nPress [Mouse 3] to use on yourself.";
                }

                itemTooltip.style.visibility = Visibility.Visible;
            }
        }

        private void HideTooltip(PointerOutEvent evt) {
            itemTooltip.style.visibility = Visibility.Hidden;
        }

        internal void Close() {
            if(!mouseSlot.SlotStack.IsEmpty()) {
                inventory.AddItem(mouseSlot.SlotStack);
            }
        }
    }
}