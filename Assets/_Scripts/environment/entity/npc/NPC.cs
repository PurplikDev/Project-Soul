using roguelike.environment.entity.player;
using roguelike.environment.world.interactable;
using UnityEngine;

namespace roguelike.environment.entity.npc {
    public class NPC : Entity, IHoverable {

        public string InteractMessage;

        public GameObject InteractionScreenHolder;

        public void Interact(Player player) {
            Debug.Log(InteractMessage);
        }

        public void OnHover(Player player) {}

        public void OnHoverEnter(Player player) {
            Debug.Log("NPC hovered");
        }

        public void OnHoverExit(Player player) {
            Debug.Log("NPC not hovered");
        }

        protected override void Awake() {
            // logic here
            base.Awake();
        }
    }
}