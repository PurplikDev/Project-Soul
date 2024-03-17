using System.Collections.Generic;
using roguelike.core.item;
using roguelike.environment.entity.player;
using roguelike.rendering.ui;
using UnityEngine.UIElements;

namespace roguelike.environment.world.deployable.workstation {
    public class StorageCrate : Deployable {

        protected virtual void Awake() {
            StationInventory = new List<ItemStack>();
            for (int i = 0; i < 12; i++) {
                StationInventory.Add(ItemStack.EMPTY);
            }
        }

        public override DeployableRenderer GetRenderer(Player interactor) {
            return new StorageCrateRenderer(interactor.Inventory, this, StationUIHolder.GetComponent<UIDocument>());
        }
    }
}
