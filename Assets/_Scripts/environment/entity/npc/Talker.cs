using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.environment.entity.npc {
    public class Talker : NPC {

        [SerializeField] string message;
        [SerializeField] float duration = 5;

        public override void Interact(Player player) {
            player.DisplayMessage(message, duration);
        }
    }
}