using roguelike.enviroment.entity.player;
using roguelike.enviroment.world;
using UnityEngine;

namespace roguelike.enviroment.entity.npc {
    public class NPC : Entity, IInteractable {
        public void Interact(Player player) {
            Debug.Log("Testing");
        }
    }
}