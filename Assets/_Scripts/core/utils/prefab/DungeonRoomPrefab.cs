using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.core.utils.prefab {
    public class DungeonRoomPrefab : MonoBehaviour {
        public bool Explored { get; private set; } = false;
        public LightPrefab[] RoomLights;

        public void Explore() {
            foreach(LightPrefab light in RoomLights) {
                light.SetState(true);
            }

            GetComponent<BoxCollider>().enabled = false;

            SpawnEnemies();
        }

        private void SpawnEnemies() {
            Debug.LogError("Spwaning enimies here!!!!!");
        }

        private void OnTriggerEnter(Collider other) {
            var player = other.GetComponent<Player>();
            if (player != null) {
                Explore();
            }
        }
    }
}