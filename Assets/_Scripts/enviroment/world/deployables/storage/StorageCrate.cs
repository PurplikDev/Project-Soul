using System.Collections.Generic;
using roguelike.core.item;
using roguelike.enviroment.entity.player;
using roguelike.rendering.ui;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.world.deployable.workstation {
    public class StorageCrate : Deployable {

        protected virtual void Awake() {
            StationInventory = new List<ItemStack>();
            for (int i = 0; i < 12; i++) {
                StationInventory.Add(new ItemStack(Items.AIR));
            }
        }

        public override DeployableRenderer GetRenderer(Player interactor) {
            return new StorageCrateRenderer(interactor.Inventory, this, StationUIHolder.GetComponent<UIDocument>());
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.G)) { // remove this when proper interaction is added
                Interact(GameObject.Find("Player").GetComponent<Player>());
            }
        }
    }
}
