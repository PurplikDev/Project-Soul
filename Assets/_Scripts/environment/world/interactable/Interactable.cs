using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.environment.world.interactable {
    public interface IHoverable {
        public abstract void Interact(Player player);

        public abstract void OnHoverEnter(Player player);

        public abstract void OnHover(Player player);

        public abstract void OnHoverExit(Player player);
    }
}