using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using roguelike.core.item.recipe;
using roguelike.enviroment.world.deployable.workstation;
using UnityEngine;
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