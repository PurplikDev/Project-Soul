using roguelike.core.utils.mathematicus;
using UnityEngine;

namespace roguelike.environment.world.dungeon {
    public class PropSpawnPoint : MonoBehaviour {
        void Awake() {
            // spawn random prop from a pool
            if(Mathematicus.ChanceIn(35)) {
                // loooooot chest!!!!!!!!!
            } else if(Mathematicus.ChanceIn(40f, 65)) {
                // some random furniture/prop
            }
        }

        private void Start() {
            Destroy(gameObject);
        }
    }
}