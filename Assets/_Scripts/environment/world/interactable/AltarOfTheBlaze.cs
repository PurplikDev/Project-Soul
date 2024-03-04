using roguelike.environment.entity.player;
using roguelike.system.manager;
using UnityEngine;

namespace roguelike.environment.world.interactable {
    public class AltarOfTheBlaze : MonoBehaviour, IHoverable {

        public ParticleSystem ActivationParticles;

        public void Interact(Player player) {
            ActivationParticles?.Play();
            GameManager.SaveGame();
        }

        public void OnHover(Player player) {}

        public void OnHoverEnter(Player player) {}

        public void OnHoverExit(Player player) {}
    }
}