using UnityEngine;

namespace roguelike.core.utils.prefab {
    public class DungeonRoomPrefab : MonoBehaviour {
        public bool Explored { get; private set; } = false;
        public LightPrefab[] RoomLights;


        private void Awake() {
            
        }

        public void Explore() {
            foreach(LightPrefab light in RoomLights) {
                light.SetState(true);
            }
        }
    }
}