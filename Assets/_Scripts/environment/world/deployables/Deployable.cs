using System;
using System.Collections.Generic;
using System.Linq;
using roguelike.core.item;
using roguelike.environment.entity.player;
using roguelike.environment.world.interactable;
using roguelike.rendering.ui;
using UnityEngine;

namespace roguelike.environment.world.deployable {
    public abstract class Deployable : MonoBehaviour, IHoverable {

        private MeshRenderer _deployableRenderer;
        public Material _outlineMaterial;
        private List<Material> _materials = new List<Material>();

        public List<ItemStack> StationInventory { get; protected set; }
        public GameObject StationUIHolder;

        public Action SlotUpdateEvent;

        private void Start() {
            _deployableRenderer = transform.GetComponentInChildren<MeshRenderer>();
            _outlineMaterial = Resources.Load<Material>("materials/props/outline");
            _materials.Add(_deployableRenderer.material);
        }

        public abstract DeployableRenderer GetRenderer(Player interactor);

        public void Interact(Player player) {
            player.UIStateMachine.OnDeployable(this);
        }

        public void OnHoverEnter(Player player) {
            _materials.Add(_outlineMaterial);
            _deployableRenderer.SetMaterials(_materials);
        }

        public void OnHover(Player player) {
            
        }

        public void OnHoverExit(Player player) {
            _materials.Remove(_outlineMaterial);
            _deployableRenderer.SetMaterials(_materials);
        }
    }
}