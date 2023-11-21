using roguelike.core.item;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class CraftingRenderer : DeployableRenderer {
        public CraftingRenderer(Inventory interactorInventory, UIDocument inventoryUI) : base(interactorInventory, inventoryUI) {

        }

        protected override void SyncInternalToVisual() {
            throw new System.NotImplementedException();
        }

        protected override void SyncInternalToVisualSingle(ItemSlot clickedSlot) {
            throw new System.NotImplementedException();
        }

        protected override void SyncVisualToInternal() {
            throw new System.NotImplementedException();
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            throw new System.NotImplementedException();
        }
    }
}