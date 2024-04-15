using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.environment.world {
    public class TriggerPlayerMessage : MonoBehaviour {

        [SerializeField] string message;

        public bool DestroyOnTriggerExit = false;
        public float Duration = 2.5f;

        private void OnTriggerEnter(Collider other) {
            var player = other.GetComponent<Player>();
            if (player != null) {
                player.DisplayMessage(message, Duration);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(DestroyOnTriggerExit) {
                Destroy(gameObject);
            }
        }
    }
}