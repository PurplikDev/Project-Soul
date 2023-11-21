using roguelike.core.item;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public abstract class DeployableRenderer : ContainerRenderer {
        public DeployableRenderer(Inventory interactorInventory, UIDocument inventoryUI) : base(interactorInventory,inventoryUI) {
            RegisterDeployableSlots();
        }

        protected abstract void RegisterDeployableSlots();
    }
}