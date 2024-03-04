using roguelike.environment.entity.player;
using roguelike.environment.world.interactable;
using roguelike.rendering;
using roguelike.rendering.ui;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.world.deployable.workstation {
    public class AltarOfTheBlaze : Deployable, IHoverable {

        public ParticleSystem ActivationParticles;

        public override DeployableRenderer GetRenderer(Player interactor) {
            return new AltarOfTheBlazeRenderer(interactor.Inventory, this, StationUIHolder.GetComponent<UIDocument>());
        }
    }
}