using roguelike.environment.entity.player;
using roguelike.environment.world.interactable;
using UnityEngine;

namespace roguelike.environment.entity.npc {
    public class NPC : Entity, IHoverable {

        // NPCs don't use the base audio events, so they are going to be repurposed for different actions
        // yes, it's a messy way to do it, but that's what i get for not making a proper system for this before

        [Space]
        [Header("NPC Properties")]
        public string[] InteractMessage;

        public GameObject InteractionScreenHolder;

        public virtual void Interact(Player player) { }

        public void OnHover(Player player) {}

        public void OnHoverEnter(Player player) {}

        public void OnHoverExit(Player player) {}

        protected override void Awake() {
            // logic here
            base.Awake();
        }
    }
}