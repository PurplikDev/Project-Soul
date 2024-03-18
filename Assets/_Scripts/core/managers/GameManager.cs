using Cinemachine;
using Newtonsoft.Json;
using roguelike.core.utils;
using roguelike.core.utils.gamedata;
using roguelike.environment.entity.player;
using roguelike.system.input;
using roguelike.system.singleton;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace roguelike.system.manager {
    public class GameManager : PersistentSingleton<GameManager> {

        public static GameState CurrentGameState;

        public Action StartGame;
        public Action GlobalPauseEvent;

        public static GameData CurrentGameData { get; private set; }
        public static GameSettings CurrentGameSettings;
        public static Player Player { get; private set; }
        public static GameObject PlayerObject { get; private set; }
        public static PlayerInput Input { get { return Player.PlayerInput; } }

        private static GameObject _playerPrefab { get { return Resources.Load<GameObject>("prefabs/entities/player"); } }

        public virtual void Start() {
            Invoke(nameof(LoadGame), 0.05f);
            CurrentGameSettings = new GameSettings(0f, 0f, 0f, HealthBarStyle.CLASSIC,  false, TranslationManager.Language.en_us);
        }

        public void LoadGame() {
            LoadingManager.Instance.LoadScene(1, GameState.MAINMENU);
        }

        private void GenerateDebugSave() {
            CurrentGameData = new GameData("Purplik", 0, Player);

            string output = JsonConvert.SerializeObject(CurrentGameData);

            Directory.CreateDirectory(Path.GetDirectoryName(GlobalStaticValues.SAVE_PATH + "/debug.json"));

            using (FileStream fileStream = new FileStream(GlobalStaticValues.SAVE_PATH + "/debug.json", FileMode.Create)) {
                byte[] info = new UTF8Encoding(true).GetBytes(output);
                fileStream.Write(info, 0, info.Length);
            }
        }

        public static void SaveGame() {
            if(CurrentGameData == null) { return; }

            CurrentGameData = new GameData(CurrentGameData.Name, CurrentGameData.Day, Player);

            string output = JsonConvert.SerializeObject(CurrentGameData);

            Directory.CreateDirectory(Path.GetDirectoryName(GlobalStaticValues.SAVE_PATH + $"/{CurrentGameData.Name}.json"));

            using (FileStream fileStream = new FileStream(GlobalStaticValues.SAVE_PATH + $"/{CurrentGameData.Name}.json", FileMode.Create)) {
                byte[] info = new UTF8Encoding(true).GetBytes(output);
                fileStream.Write(info, 0, info.Length);
            }
        }

        public static GameData CreateNewSave(string characterName) {
            GameData data = GameData.EMPTY;
            data.Name = characterName;
            return data;
        }

        public void LoadSave(GameData gameData) {
            CurrentGameData = gameData;
        }

        public void GoToMainMenu() {
            LoadingManager.Instance.LoadScene(1, GameState.MAINMENU);
        }

        internal static void SpawnPlayer(AsyncOperation _) {
            if (CurrentGameState != GameState.MAINMENU) {
                if (CurrentGameData == null) {
                    Debug.LogError("No GameData present!");
                    return;
                }
                PlayerObject = Instantiate(_playerPrefab, new Vector3(0, 1, 0), _playerPrefab.transform.rotation);
                PlayerObject.transform.SetParent(null);
                Player = PlayerObject.GetComponent<Player>();
                GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Follow = PlayerObject.transform;
            }
        }

        public static void UpdateGameData() {
            CurrentGameData = new GameData(CurrentGameData.Name, CurrentGameData.Day, Player);
        }
    }

    public enum GameState {
        MAINMENU,
        TOWN,
        DUNGEON
    }
}