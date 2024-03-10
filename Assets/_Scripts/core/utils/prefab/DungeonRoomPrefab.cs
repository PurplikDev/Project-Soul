using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.core.utils.prefab {
    public class DungeonRoomPrefab : MonoBehaviour {
        public bool Explored { get; private set; } = false;
        public LightPrefab[] RoomLights;

        private void Awake() {
            SpawnEnemies();
        }

        public void Explore() {
            foreach(LightPrefab light in RoomLights) {
                light.SetState(true);
            }

            GetComponent<BoxCollider>().enabled = false;
        }

        private void SpawnEnemies() {

        }

        private void OnTriggerEnter(Collider other) {
            var player = other.GetComponent<Player>();
            if (player != null) {
                Explore();
            }
        }
    }
}