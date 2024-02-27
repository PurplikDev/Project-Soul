using roguelike.core.item;
using roguelike.environment.entity.npc;
using roguelike.rendering.ui.slot;
using roguelike.system.manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui
{
    public class TradeRenderer : ContainerRenderer
    {
        private int playerValueAmount, traderValueAmount;

        internal List<ItemSlot> traderInventorySlots = new List<ItemSlot>();
        internal List<ItemSlot> playerInventorySlots = new List<ItemSlot>();
        internal List<ItemSlot> traderOffer = new List<ItemSlot>();
        internal List<ItemSlot> playerOffer = new List<ItemSlot>();

        protected VisualElement traderInventoryRoot, traderOfferRoot, playerOfferRoot;

        protected Label playerValue, traderValue;

        protected Trader trader;

        public TradeRenderer(Inventory entityInventory, Trader trader, UIDocument inventoryUI) : base(entityInventory, inventoryUI) {
            this.trader = trader;

            traderInventoryRoot = root.Q<VisualElement>("TraderInventorySlotContainer");
            traderOfferRoot = root.Q<VisualElement>("TraderOfferSlots");
            playerOfferRoot = root.Q<VisualElement>("PlayerOfferSlots");

            playerValue = root.Q<Label>("PlayerValueDisplay");
            traderValue = root.Q<Label>("TraderValueDisplay");

            root.Q<Button>("DealButton").clicked += Deal;

            TranslateHeader(root.Q<Label>("TraderInventoryHeader"));
            TranslateHeader(root.Q<Label>("InventoryHeader"));
            TranslateHeader(root.Q<Label>("TraderValueHeader"));
            TranslateHeader(root.Q<Label>("PlayerValueHeader"));
            TranslateHeader(root.Q<Label>("DealButtonHeader"));

            playerInventorySlots = itemSlots.ToList();

            RegisterTraderInventory();
            RegisterTraderOffer();
            RegisterPlayerOffer();
        }

        // Slot Registrie
        // todo: offer slot registration, offer value calculation, offer creation

        protected void RegisterTraderInventory() {
            int slotIndex = 0;
            foreach (ItemSlot offerSlot in traderInventoryRoot.Children().ToList()) {
                offerSlot.SlotIndex = itemSlots.Count;
                offerSlot.SetStack(trader.Stock[slotIndex]);
                offerSlot.Renderer = this;
                itemSlots.Add(offerSlot);
                traderInventorySlots.Add(offerSlot);
                offerSlot.UpdateSlotEvent.Invoke();
                slotIndex++;
            }
        }

        protected void RegisterTraderOffer() {
            foreach (ItemSlot offerSlot in traderOfferRoot.Children().ToList()) {
                offerSlot.SlotIndex = itemSlots.Count;
                offerSlot.SetStack(ItemStack.EMPTY);
                offerSlot.Renderer = this;
                itemSlots.Add(offerSlot);
                traderOffer.Add(offerSlot);
                offerSlot.UpdateSlotEvent.Invoke();
            }
        }

        protected void RegisterPlayerOffer() {
            foreach (ItemSlot offerSlot in playerOfferRoot.Children().ToList()) {
                offerSlot.SlotIndex = itemSlots.Count;
                offerSlot.SetStack(ItemStack.EMPTY);
                offerSlot.Renderer = this;
                itemSlots.Add(offerSlot);
                playerOffer.Add(offerSlot);
                offerSlot.UpdateSlotEvent.Invoke();
            }
        }



        // Slot Interaction Methods

        public override void ClickSlot(Vector2 position, ItemSlot originalSlot, bool isPrimary) {
            if(originalSlot.SlotStack.IsEmpty()) { return; }

            if(playerInventorySlots.Contains(originalSlot)) {
                if (SpaceInOffer(playerOffer, originalSlot.SlotStack)) {
                    SwapItemsInSlots(originalSlot, playerOffer, isPrimary);
                }
            } else if (traderInventorySlots.Contains(originalSlot)) {
                if (SpaceInOffer(traderOffer, originalSlot.SlotStack)) {
                    SwapItemsInSlots(originalSlot, traderOffer, isPrimary);
                }
            } else if (playerOffer.Contains(originalSlot)) {
                SwapItemsInSlots(originalSlot, playerInventorySlots, isPrimary);
            } else if (traderOffer.Contains(originalSlot)) {
                SwapItemsInSlots(originalSlot, traderInventorySlots, isPrimary);
            }

            UpdateValues();
        }

        private void Deal() {
            // todo: add checks if the player and the trader have enough space
            // for the items that are meants to be exchanged

            // todo: options to haggle with the trader, increasing or decreasing the treshold of the trade

            if (playerValueAmount >= traderValueAmount) {
                foreach(ItemSlot slot in traderOffer) {
                    if(!slot.SlotStack.IsEmpty()) {
                        foreach(ItemSlot playerSlot in playerInventorySlots) {
                            if(playerSlot.SlotStack.IsEmpty()) {
                                playerSlot.SetStack(slot.SlotStack);
                                slot.SetStack(ItemStack.EMPTY);
                            }
                        }
                        slot.SetStack(ItemStack.EMPTY);
                    }
                }

                foreach (ItemSlot slot in playerOffer) {
                    if (!slot.SlotStack.IsEmpty()) {
                        foreach (ItemSlot traderSlot in traderInventorySlots) {
                            if (traderSlot.SlotStack.IsEmpty()) {
                                traderSlot.SetStack(slot.SlotStack);
                                slot.SetStack(ItemStack.EMPTY);
                            }
                        }
                        slot.SetStack(ItemStack.EMPTY);
                    }
                }
                SyncVIsualToInternalAll();                
            }    
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            if (playerInventorySlots.Contains(clickedSlot)) {
                GameManager.Instance.Player.Inventory.Items[clickedSlot.SlotIndex] = clickedSlot.SlotStack;
            } else if (traderInventorySlots.Contains(clickedSlot)) {
                trader.Stock[clickedSlot.SlotIndex - 20] = clickedSlot.SlotStack;
            }
        }

        protected void SyncVIsualToInternalAll() {
            foreach(ItemSlot slot in playerInventorySlots) {
                SyncVisualToInternalSingle(slot);
            }
            foreach (ItemSlot slot in traderInventorySlots) {
                SyncVisualToInternalSingle(slot);
            }
        }



        // Utility Methods

        private bool SpaceInOffer(List<ItemSlot> slotsToCheck, ItemStack stack) {
            foreach (ItemSlot slot in slotsToCheck) {
                if (slot.SlotStack.IsEmpty() || 
                    slot.SlotStack.Item == stack.Item && 
                    slot.SlotStack.StackSize < slot.SlotStack.Item.MaxStackSize) {
                    return true;
                }
            }
            return false;
        }

        private void SwapItemsInSlots(ItemSlot clickedSlot, List<ItemSlot> listToCheck, bool isPrimary) {
            foreach (ItemSlot slot in listToCheck) {
                if(slot.SlotStack.Item == clickedSlot.SlotStack.Item && slot.SlotStack.StackSize < clickedSlot.SlotStack.Item.MaxStackSize) {
                    if(isPrimary) {
                        FillSlot(slot, clickedSlot);
                    } else {
                        slot.SlotStack.IncreaseStackSize(1);
                        clickedSlot.SlotStack.DecreaseStackSize(1);
                    }
                    slot.UpdateSlotEvent.Invoke();
                    clickedSlot.UpdateSlotEvent.Invoke();
                    return;
                }
            }

            foreach (ItemSlot slot in listToCheck) {
                if (slot.SlotStack.IsEmpty()) {
                    if(isPrimary) {
                        slot.SetStack(clickedSlot.SlotStack);
                        clickedSlot.SetStack(ItemStack.EMPTY);
                        return;
                    } else {
                        slot.SetStack(new ItemStack(clickedSlot.SlotStack.Item));
                        clickedSlot.SlotStack.DecreaseStackSize(1);
                        clickedSlot.UpdateSlotEvent.Invoke();
                        return;
                    }
                    
                }
            }

            
            return;
        }

        private void UpdateValues() {
            playerValueAmount = 0;
            foreach(ItemSlot slot in playerOffer) { playerValueAmount += slot.SlotStack.Item.ItemValue * slot.SlotStack.StackSize; }
            playerValue.text = playerValueAmount.ToString();

            traderValueAmount = 0;
            foreach (ItemSlot slot in traderOffer) { traderValueAmount += slot.SlotStack.Item.ItemValue * slot.SlotStack.StackSize; }
            traderValue.text = traderValueAmount.ToString();
        }
    }
}