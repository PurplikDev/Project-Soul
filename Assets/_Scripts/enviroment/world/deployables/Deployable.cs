using System;
using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using roguelike.enviroment.entity.player;
using roguelike.enviroment.world.interactable;
using roguelike.rendering.ui;
using UnityEngine;

namespace roguelike.enviroment.world.deployable {
    public abstract class Deployable : Interactable {

        private MeshRenderer _deployableRenderer;
        public Material _outlineMaterial;
        private List<Material> _materials = new List<Material>();

        public List<ItemStack> StationInventory { get; protected set; }
        public GameObject StationUIHolder;

        public Action SlotUpdateEvent;

        private void Start() {
            _deployableRenderer = transform.GetComponentInChildren<MeshRenderer>();
            _outlineMaterial = Resources.Load<Material>("materials/outline");
            _materials.Add(_deployableRenderer.material);
        }

        public abstract DeployableRenderer GetRenderer(Player interactor);

        public override void Interact(Player player) {
            player.UIStateMachine.OnDeployable(this);
        }

        public override void OnHoverEnter(Player player) {
            _materials.Add(_outlineMaterial);
            _deployableRenderer.SetMaterials(_materials);
        }

        public override void OnHover(Player player) {
            
        }

        public override void OnHoverExit(Player player) {
            _materials.Remove(_outlineMaterial);
            _deployableRenderer.SetMaterials(_materials);
        }
    }
}