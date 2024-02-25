using roguelike.core.item;
using roguelike.environment.entity.npc;
using roguelike.rendering.ui.slot;
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
            foreach (ItemSlot offerSlot in traderInventoryRoot.Children().ToList()) {
                offerSlot.SlotIndex = itemSlots.Count;
                offerSlot.SetStack(ItemStack.EMPTY);
                offerSlot.Renderer = this;
                itemSlots.Add(offerSlot);
                traderInventorySlots.Add(offerSlot);
                offerSlot.UpdateSlotEvent.Invoke();
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
            Debug.Log(originalSlot.SlotStack.Item.Name);
            if(playerInventorySlots.Contains(originalSlot)) {
                if(!originalSlot.SlotStack.IsEmpty() && PlayerOfferSpace()) {
                    AddPlayerOffer(originalSlot.SlotStack);
                    originalSlot.SetStack(ItemStack.EMPTY);
                    UpdatePlayerValue();
                }
            } else if (traderInventorySlots.Contains(originalSlot)) {
                if (!originalSlot.SlotStack.IsEmpty() && TraderOfferSpace()) {
                    AddTraderOffer(originalSlot.SlotStack);
                    originalSlot.SetStack(ItemStack.EMPTY);
                    UpdateTraderValue();
                }
            } else if (playerOffer.Contains(originalSlot)) {
                Debug.Log("player offer");
            } else if (traderOffer.Contains(originalSlot)) {
                Debug.Log("trader offer");
            }
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot)
        {
            Debug.LogWarning("this is missing!!!");
        }



        // Utility Methods

        private bool TraderOfferSpace() {
            foreach(ItemSlot slot in traderOffer) {
                if(slot.SlotStack.IsEmpty()) {
                    return true;
                }
            }
            return false;
        }

        private bool PlayerOfferSpace() {
            foreach (ItemSlot slot in playerOffer) {
                if (slot.SlotStack.IsEmpty()) {
                    return true;
                }
            }
            return false;
        }

        private void AddPlayerOffer(ItemStack stack) {
            foreach (ItemSlot slot in playerOffer) {
                if (slot.SlotStack.IsEmpty()) {
                    slot.SetStack(stack);
                    return;
                }
            }
        }

        private void AddTraderOffer(ItemStack stack) {
            foreach (ItemSlot slot in traderOffer) {
                if (slot.SlotStack.IsEmpty()) {
                    slot.SetStack(stack);
                    return;
                }
            }
        }

        private void UpdatePlayerValue() {
            playerValueAmount = 0;
            foreach(ItemSlot slot in playerOffer) { playerValueAmount += slot.SlotStack.Item.ItemValue; }
            playerValue.text = playerValueAmount.ToString();
        }

        private void UpdateTraderValue() {
            traderValueAmount = 0;
            foreach (ItemSlot slot in traderOffer) { traderValueAmount += slot.SlotStack.Item.ItemValue; }
            traderValue.text = traderValueAmount.ToString();
        }
    }
}