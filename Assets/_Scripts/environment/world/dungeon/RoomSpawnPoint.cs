using roguelike.core.utils.mathematicus;
using roguelike.system.manager;
using UnityEngine;

namespace roguelike.environment.world.dungeon {
    public class RoomSpawnPoint : MonoBehaviour {

        public RoomOrientation orientation;

        static int points;
        public static bool keySpawned;

        void Awake() {
            points++;
            keySpawned = false; // i set this to false in so many places, when i remove one of them, it doesn't work
        }

        private void OnEnable() {
            keySpawned = false;
        }

        private void OnDisable() {
            keySpawned = false;
        }

        void Start() {

            if(!keySpawned) {
                if(Mathematicus.ChanceIn(1, points)) {
                    keySpawned = true;
                    Debug.Log("spawn key door here");
                    var room = Resources.Load<GameObject>($"prefabs/dungeon/rooms/key/Keyroom {orientation.ToString().ToLower()}");
                    Instantiate(room, transform.position, room.transform.rotation, transform.parent);
                    return;
                }
            }

            points--;

            // spawn random room from a pool
            if (Mathematicus.ChanceIn(0)) {
                var treasure_rooms = Resources.LoadAll<GameObject>($"prefabs/dungeon/rooms/treasure/{orientation.ToString().ToLower()}");
                if (treasure_rooms.Length > 0) {
                    var room = treasure_rooms[Random.Range(0, treasure_rooms.Length)];
                    Instantiate(room, transform.position, room.transform.rotation, transform.parent);
                }
            } else {
                var rooms = Resources.LoadAll<GameObject>($"prefabs/dungeon/rooms/normal/{orientation.ToString().ToLower()}");
                if (rooms.Length > 0) {
                    var room = rooms[Random.Range(0, rooms.Length)];
                    Instantiate(room, transform.position, room.transform.rotation, transform.parent);
                }
            }
        }
    }

    public enum RoomOrientation {
        XPlus, XMinus, ZPlus, ZMinus
    }
}