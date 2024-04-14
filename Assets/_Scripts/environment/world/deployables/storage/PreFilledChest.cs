using System.Collections.Generic;
using roguelike.core.item;
using roguelike.environment.entity.player;
using roguelike.rendering.ui;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.world.deployable.workstation {
    public class PreFilledChest : StorageCrate {

        [SerializeField] string[] items;

        protected override void Awake() {
            StationInventory = new List<ItemStack>();
            int index = 0;

            foreach(string item in items) {
                index++;
                StationInventory.Add(new ItemStack(ItemManager.GetItemByID(item)));
            }

            for(; index < 12; index++) {
                StationInventory.Add(ItemStack.EMPTY);
            }
        }

        public override DeployableRenderer GetRenderer(Player interactor) {
            return new StorageCrateRenderer(interactor.Inventory, this, StationUIHolder.GetComponent<UIDocument>());
        }
    }
}
