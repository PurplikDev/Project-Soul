using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public class CraftingRenderer : ContainerRenderer {
        public CraftingRenderer(UIDocument inventoryUI) : base(inventoryUI) {
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