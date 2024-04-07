using roguelike.environment.entity.player;
using UnityEngine;
using UnityEngine.Events;

namespace roguelike.environment.world {
    public class EnviromentalTrigger : MonoBehaviour {
        public UnityEvent OnEnterTriggerEvent, OnExitTriggerEvent;

        private void OnTriggerEnter(Collider other) {
            var player = other.GetComponent<Player>();
            if (player != null) {
                OnEnterTriggerEvent.Invoke();
            }
        }

        private void OnTriggerExit(Collider other) {
            var player = other.GetComponent<Player>();
            if (player != null) {
                OnExitTriggerEvent.Invoke();
            }
        }
    }
}