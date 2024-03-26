using roguelike.core.utils.mathematicus;
using UnityEngine;

namespace roguelike.environment.world.dungeon {
    public class RoomSpawnPoint : MonoBehaviour {

        public RoomOrientation orientation;

        void Awake() {
            // spawn random room from a pool
            if(Mathematicus.ChanceIn(15)) {
                // treasure room!!!!!!!!!!!!! yarrrrrrrrrrrr
            } else {
                // normal room :c
            }
        }

        private void Start() {
            Destroy(gameObject);
        }
    }

    public enum RoomOrientation {
        XPlus, XMinus, ZPlus, ZMinus
    }
}