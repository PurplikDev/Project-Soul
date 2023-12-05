using roguelike.enviroment.entity.player;
using UnityEngine;

namespace roguelike.enviroment.world.interactable {
    public abstract class Interactable : MonoBehaviour {
        public abstract void Interact(Player player);

        public abstract void OnHoverEnter(Player player);

        public abstract void OnHover(Player player);

        public abstract void OnHoverExit(Player player);
    }
}