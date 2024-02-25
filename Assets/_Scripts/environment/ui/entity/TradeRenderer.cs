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
        internal List<ItemSlot> traderInventorySlots = new List<ItemSlot>();
        internal List<ItemSlot> traderOffer = new List<ItemSlot>();
        internal List<ItemSlot> playerOffer = new List<ItemSlot>();

        protected VisualElement traderInventoryRoot, traderOfferRoot, playerOfferRoot;

        protected Trader trader;

        public TradeRenderer(Inventory entityInventory, Trader trader, UIDocument inventoryUI) : base(entityInventory, inventoryUI) {
            this.trader = trader;

            traderInventoryRoot = root.Q<VisualElement>("TraderInventorySlotContainer");
            traderOfferRoot = root.Q<VisualElement>("TraderOfferSlots");
            playerOfferRoot = root.Q<VisualElement>("PlayerOfferSlots");

            TranslateHeader(root.Q<Label>("TraderInventoryHeader"));
            TranslateHeader(root.Q<Label>("InventoryHeader"));
            TranslateHeader(root.Q<Label>("TraderValueHeader"));
            TranslateHeader(root.Q<Label>("PlayerValueHeader"));
            TranslateHeader(root.Q<Label>("DealButtonHeader"));

            RegisterTraderInventory();
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




        public override void ClickSlot(Vector2 position, ItemSlot originalSlot, bool isPrimary) {
            Debug.Log("busuaugygfuiguif");
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot)
        {
            Debug.LogWarning("this is missing!!!");
        }
    }
}