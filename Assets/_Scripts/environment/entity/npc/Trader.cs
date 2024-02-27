using roguelike.core.item;
using roguelike.core.item.loottable;
using roguelike.core.utils.mathematicus;
using roguelike.environment.entity.player;
using roguelike.rendering.ui;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace roguelike.environment.entity.npc
{
    public class Trader : NPC
    {
        public int Gold { get; private set; }
        public LootTable possibleOffer;
        public List<ItemStack> Stock {  get; private set; } = new List<ItemStack>();

        protected override void Awake() {
            base.Awake();
            FillInventory();
        }

        public TradeRenderer GetRenderer(Player interactor)
        {
            return new TradeRenderer(interactor.Inventory, this, InteractionScreenHolder.GetComponent<UIDocument>());
        }

        public override void Interact(Player player) {
            Stock.Clear();
            FillInventory();
            player.UIStateMachine.OnTrader(this);
        }

        protected void FillInventory() {
            for(int slotsFilled = 0; slotsFilled < 20; slotsFilled++) {
                if(Mathematicus.ChanceIn(65f)) {
                    Stock.Add(ItemStack.EMPTY);
                } else {
                    Stock.Add(possibleOffer.GetRandomLoot());
                }
            }
        }
    }
}   