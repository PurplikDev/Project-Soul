using System;
using System.Collections.Generic;
using roguelike.core.item;
using roguelike.core.item.recipe;
using roguelike.enviroment.entity.player;
using roguelike.rendering.ui;
using roguelike.system.manager;
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
    }
}
