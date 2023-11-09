using System;
using System.Collections.Generic;
using roguelike.core.item;
using roguelike.enviroment.entity.player;
using roguelike.rendering.ui;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.world.deployable.workstation {
    public class CraftingStation : MonoBehaviour {
        public CraftingRenderer craftingRenderer;

        public List<ItemStack> StationInventory;

        public Action SlotUpdateEvent;

        protected virtual void Awake() {
            StationInventory = new List<ItemStack>();
            for(int i = 0; i < 10; i++) {
                StationInventory.Add(new ItemStack(Items.TEST2));
            }

            // todo: add a method for calling recipe manager to provide a recipe
        }

        public void OpenUI(Player interactor) {
            UIDocument document = GetComponentInChildren<UIDocument>();
            document.enabled = true;
            craftingRenderer = new CraftingRenderer(interactor.PlayerInventory, document, this);
        }
    }
}
