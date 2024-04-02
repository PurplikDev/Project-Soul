using roguelike.system.manager;
using UnityEngine;

namespace roguelike.environment.world.dungeon {
    public class EntitySpawnPoint : MonoBehaviour {

        // todo: instead of a monster pool, apply damage, defence and health modifiers to spawned entities

        string[,] path = {
            { "tier_0", "tier_1", "tier_2" },  // [0,x]
            { "tier_1", "tier_1", "tier_2" }, // [1,x]
            { "tier_1", "tier_2", "tier_3" } // [2,x]
        };

        void Awake() {
            var difficulty = (int)DungeonManager.Instance.Difficulty;
            var currentLayer = DungeonManager.CurrentLayer;

            // kind of ungly code... i was too tired to make this properly (8 am tuesday)

            int layer;
            if(currentLayer < 3) {
                layer = 0;
            } else if (currentLayer < 5) {
                layer = 1;
            } else {
                layer = 2;
            }

            Debug.Log(path[difficulty, layer]);
        }

        private void Start() {
            Destroy(gameObject);
        }
    }
}