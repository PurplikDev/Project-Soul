using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.environment.world.interactable {
    public class Door : MonoBehaviour, IHoverable {
        public void Interact(Player player) {

            // todo: do

            Debug.Log("open the doooooor!!!!!!!!!!!!!!");
        }

        public void OnHover(Player player) {}

        public void OnHoverEnter(Player player) {}

        public void OnHoverExit(Player player) {}
    }
}