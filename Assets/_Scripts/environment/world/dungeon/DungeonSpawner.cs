using UnityEngine;

namespace roguelike.environment.world.dungeon {
    public class DungeonSpawner : MonoBehaviour {

        public void SpawnDungeon(Dungeon dungeon) {

        }

        /*
        /// <summary>
        /// Method for spawning the dungeon into the scene.
        /// </summary>
        private void InstantiateDungeon() {
            GameObject dungeonAnchor = new GameObject();
            dungeonAnchor.name = "Dungeon";
            foreach(Room room in generatedRooms) {
                if(room.TileType == TileType.ROOM) {
                    var roomAnchor = Instantiate(Floor, new Vector3(room.y * 10, 0, room.x * 10), new Quaternion(0, 0, 0, 0), dungeonAnchor.transform);
                    foreach(KeyValuePair<Direction, WallType> keyValuePair in room.wallData) {
                        if(keyValuePair.Value == WallType.NONE) { continue; }
                        Vector3 pos = new Vector3();
                        switch(keyValuePair.Key) {
                            case Direction.UP: pos = new Vector3(-5, 0, 0); break;
                            case Direction.DOWN: pos = new Vector3(5, 0, 0); break;
                            case Direction.LEFT: pos = new Vector3(0, 0, -5); break;
                            case Direction.RIGHT: pos = new Vector3(0, 0, 5); break;
                        }

                        Instantiate(
                            keyValuePair.Value == WallType.WALL ? Wall : Door,
                            pos + roomAnchor.position, new Quaternion(0, 0, 0, 0), roomAnchor);
                    }
                }
            }

            dungeonAnchor.transform.parent = GameObject.Find("Enviroment").transform;
            dungeonAnchor.transform.rotation = new Quaternion();
        }
        */
    }
}