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
        internal List<OfferSlot> traderInventorySlots = new List<OfferSlot>();
        internal List<OfferSlot> traderOffer = new List<OfferSlot>();
        internal List<OfferSlot> playerOffer = new List<OfferSlot>();

        protected VisualElement traderInventoryRoot, traderOfferRoot, playerOfferRoot;

        protected Trader trader;

        public TradeRenderer(Inventory entityInventory, Trader trader, UIDocument inventoryUI) : base(entityInventory, inventoryUI) {
            this.trader = trader;

            traderInventoryRoot = root.Q<VisualElement>("TraderInventorySlotContainer");
            traderOfferRoot = root.Q<VisualElement>("TraderOfferSlots");
            playerOfferRoot = root.Q<VisualElement>("PlayerOfferSlots");

            TranslateHeader(root.Q<Label>("TraderInventoryHeader"));
            TranslateHeader(root.Q<Label>("DealButton"));

            RegisterTraderInventory();
        }

        // Slot Registrie
        // todo: offer slot registration, offer value calculation, offer creation

        protected void RegisterTraderInventory() {
            foreach (OfferSlot offerSlot in traderInventoryRoot.Children().ToList()) {
                offerSlot.SlotIndex = itemSlots.Count;
                offerSlot.ForceStack(ItemStack.EMPTY);
                offerSlot.Renderer = this;
                itemSlots.Add(offerSlot);
                traderInventorySlots.Add(offerSlot);
                offerSlot.UpdateSlotEvent.Invoke();
            }
        }




        public override void ClickSlot(Vector2 position, ItemSlot originalSlot, bool isPrimary) {
            if(originalSlot is OfferSlot) {
                Debug.Log("busuaugygfuiguif");
            }
            base.ClickSlot(position, originalSlot, isPrimary);
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot)
        {
            Debug.LogWarning("this is missing!!!");
        }
    }
}