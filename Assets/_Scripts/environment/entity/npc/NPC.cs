using roguelike.environment.entity.player;
using roguelike.environment.world.interactable;
using UnityEngine;

namespace roguelike.environment.entity.npc {
    public class NPC : Entity, IHoverable {

        [Space]
        [Header("NPC Properties")]
        public string InteractMessage;

        public GameObject InteractionScreenHolder;

        public virtual void Interact(Player player) {
            Debug.Log(InteractMessage);
        }

        public void OnHover(Player player) {}

        public void OnHoverEnter(Player player) {}

        public void OnHoverExit(Player player) {}

        protected override void Awake() {
            // logic here
            base.Awake();
        }
    }
}