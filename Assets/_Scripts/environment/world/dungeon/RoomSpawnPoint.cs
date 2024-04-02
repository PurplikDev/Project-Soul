using roguelike.core.utils.mathematicus;
using UnityEngine;

namespace roguelike.environment.world.dungeon {
    public class RoomSpawnPoint : MonoBehaviour {

        public RoomOrientation orientation;

        void Awake() {
            // spawn random room from a pool
            if(Mathematicus.ChanceIn(0)) {
                var treasure_rooms = Resources.LoadAll<GameObject>($"prefabs/dungeon/rooms/treasure/{orientation.ToString().ToLower()}");
                if (treasure_rooms.Length > 0) {
                    var room = treasure_rooms[Random.Range(0, treasure_rooms.Length)];
                    Instantiate(room, transform.position, room.transform.rotation, transform.parent);
                }
            } else {
                var rooms = Resources.LoadAll<GameObject>($"prefabs/dungeon/rooms/normal/{orientation.ToString().ToLower()}");
                if(rooms.Length > 0) {
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