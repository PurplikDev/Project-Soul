using roguelike.core.utils.mathematicus;
using roguelike.system.singleton;
using UnityEngine;

namespace roguelike.system.manager {
    public class DungeonManager : Singleton<DungeonManager> {

        public DungeonDifficulty Difficulty;
        public static DungeonState State { get; set; }

        public int Layer { get; private set; }
        public int LayersUntilBoss { get; private set; }

        private GameObject _camp { get { return Resources.Load<GameObject>("prefabs/dungeon/rooms/camp"); } }
        private GameObject[] _layouts { get { return Resources.LoadAll<GameObject>("prefabs/dungeon/layouts"); } }

        public static void SpawnDungeon(AsyncOperation _) {
            if(State == DungeonState.DUNGEON) {
                // spawn dungeon
            } else if(State == DungeonState.CAMP) {
                // spawn camp
            } else if(State == DungeonState.BOSS) {
                // spawn boss
            } else {
                Debug.LogError("Invalid Dungeon State!");
            }
        }

        public void EnterDungeon(DungeonDifficulty difficulty) {
            Difficulty = difficulty;
            Layer = 1;
            LayersUntilBoss = Random.Range(5, 8);
            LoadingManager.Instance.LoadScene(3, GameState.DUNGEON);
            State = DungeonState.DUNGEON;
        }

        public void EnterLayer() {
            Layer++;
            LayersUntilBoss--;
            State = DungeonState.DUNGEON;
        }

        public void EnterCamp() {
            State = DungeonState.CAMP;
        }

        public void ExitCamp() {
            GameManager.UpdateGameData();
            EnterLayer();
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

