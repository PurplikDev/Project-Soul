using roguelike.core.utils.mathematicus;
using roguelike.environment.world.dungeon;
using roguelike.system.singleton;
using System.Collections.Generic;
using UnityEngine;

namespace roguelike.system.manager {
    public class DungeonManager : Singleton<DungeonManager> {

        public DungeonDifficulty Difficulty;
        public static DungeonState State { get; set; }

        public static int CurrentLayer { get; private set; }
        public static int LayersUntilBoss { get; private set; }

        static GameObject camp { get { return Resources.Load<GameObject>("prefabs/dungeon/rooms/camp"); } }
        static GameObject[] floors { get { return Resources.LoadAll<GameObject>("prefabs/dungeon/layouts"); } }
        static GameObject[] firstFloors { get { return Resources.LoadAll<GameObject>("prefabs/dungeon/firstFloorLayouts"); } }
        static GameObject[] bossFloors { get { return Resources.LoadAll<GameObject>("prefabs/dungeon/bossLayouts"); } }

        public static void SpawnDungeon(AsyncOperation _) {
            RoomSpawnPoint.keySpawned = false;
            if(State == DungeonState.DUNGEON) {
                if(CurrentLayer == 1) {
                    Instantiate(firstFloors[Random.Range(0, firstFloors.Length)]);
                } else {
                    Instantiate(floors[Random.Range(0, floors.Length)]);
                }
            } else if(State == DungeonState.CAMP) {
                Instantiate(camp);
            } else if(State == DungeonState.BOSS) {
                Instantiate(bossFloors[Random.Range(0, bossFloors.Length)]);
            } else {
                Debug.LogError("Invalid Dungeon State!");
            }
        }

        public void EnterDungeon(DungeonDifficulty difficulty) {
            Difficulty = difficulty;
            CurrentLayer = 1;
            LayersUntilBoss = Random.Range(5, 8);
            LoadingManager.Instance.LoadScene(3, GameState.DUNGEON);
            State = DungeonState.DUNGEON;
        }

        public void EnterLayer() {
            CurrentLayer++;
            LayersUntilBoss--;
            State = DungeonState.DUNGEON;
            RoomSpawnPoint.keySpawned = false;
        }

        public void EnterCamp() {
            State = DungeonState.CAMP;
        }

        public void ExitCamp() {
            GameManager.UpdateGameData();
            EnterLayer();
            LoadingManager.Instance.LoadScene(3, GameState.DUNGEON);
        }

        public void ExitLayer() {
            GameManager.UpdateGameData();
            if (Mathematicus.ChanceIn(1, 3)) {
                EnterCamp();
            } else {
                EnterLayer();
            }
            LoadingManager.Instance.LoadScene(3, GameState.DUNGEON);
        }

        public void ExitDungeon() {
            LoadingManager.Instance.LoadScene(2, GameState.TOWN);
            State = DungeonState.NONE;
        }

        public enum DungeonDifficulty {
            EASY,
            NORMAL,
            HARD
        }

        public enum DungeonState {
            NONE,
            DUNGEON,
            CAMP,
            BOSS
        }
    }
}

