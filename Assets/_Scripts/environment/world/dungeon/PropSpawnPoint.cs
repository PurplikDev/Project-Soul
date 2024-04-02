using roguelike.core.utils.mathematicus;
using UnityEditor;
using UnityEngine;

namespace roguelike.environment.world.dungeon {
    public class PropSpawnPoint : MonoBehaviour {
        void Awake() {
            // spawn random prop from a pool
            if(Mathematicus.ChanceIn(35)) {
                var treasures = Resources.LoadAll<GameObject>("prefabs/props/treasure");
                if (treasures.Length > 0) {
                    var treasure = treasures[Random.Range(0, treasures.Length)];
                    Instantiate(treasure, transform.position, transform.rotation);
                }
            } else if(Mathematicus.ChanceIn(40f, 65)) {
                // some random furniture/prop
            }
        }

        private void Start() {
            Destroy(gameObject);
        }
    }
}