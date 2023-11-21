using roguelike.core.item;
using roguelike.enviroment.world.deployable;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui {
    public abstract class DeployableRenderer : ContainerRenderer {
        protected Deployable deployable;

        public DeployableRenderer(Inventory interactorInventory, Deployable deployable, UIDocument inventoryUI) : base(interactorInventory, inventoryUI) {
            this.deployable = deployable;
        }

        protected abstract void RegisterDeployableSlots();
    }
}