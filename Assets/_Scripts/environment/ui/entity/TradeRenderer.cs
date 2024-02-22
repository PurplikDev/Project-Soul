using roguelike.core.item;
using roguelike.environment.entity.npc;
using roguelike.rendering.ui.slot;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui
{
    public class TradeRenderer : ContainerRenderer
    {
        public TradeRenderer(Inventory entityInventory, Trader trader, UIDocument inventoryUI) : base(entityInventory, inventoryUI) {
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot)
        {
            Debug.LogWarning("this is missing!!!");
        }
    }
}