using System;
using System.Collections.Generic;
using roguelike.core.item;
using roguelike.enviroment.entity.player;
using roguelike.rendering.ui;
using UnityEngine;

namespace roguelike.enviroment.world.deployable {
    public abstract class Deployable : MonoBehaviour, IInteractable {
        public List<ItemStack> StationInventory { get; protected set; }
        public GameObject StationUIHolder;

        public Action SlotUpdateEvent;

        public abstract DeployableRenderer GetRenderer(Player interactor);

        public void Interact(Player player) {
            player.UIStateMachine.OnDeployable(this);
        }
    }
}