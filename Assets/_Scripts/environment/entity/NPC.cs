using roguelike.core.utils.dialogutils;
using roguelike.environment.entity.player;
using roguelike.environment.world.interactable;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.entity.npc {
    public class NPC : Entity, IHoverable {

        public void Interact(Player player) {

            Debug.Log("interacting with npc");

            var portrait = Resources.Load<Sprite>("sprites/items/test");

            DialogManager.StartDialog(GameManager.Instance.Player, new DialogData(portrait, "joe", "joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe joe"));
        }

        public void OnHover(Player player) {
            Debug.Log("NPC hovering");
        }

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