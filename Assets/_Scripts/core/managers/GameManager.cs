using Cinemachine;
using Newtonsoft.Json;
using roguelike.core.item;
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

        protected static GameData gameData { get; private set; }
        public static Player Player { get; private set; }
        public static GameObject PlayerObject { get; private set; }
        public static PlayerInput Input { get { return Player.PlayerInput; } }

        public static GameSettings GameSettings { get; private set; }

        private static GameObject _playerPrefab { get { return Resources.Load<GameObject>("prefabs/entities/player"); } }

        public virtual void Start() {
            Invoke(nameof(LoadGame), 0.05f);
        }

        public void LoadGame() {
            LoadingManager.Instance.LoadScene(1, GameState.MAINMENU);
        }

        private void GenerateDebugSave() {
            gameData = new GameData("Purplik", 0, Player);

            string output = JsonConvert.SerializeObject(gameData);

            Directory.CreateDirectory(Path.GetDirectoryName(GlobalStaticValues.SAVE_PATH + "/debug.json"));

            using (FileStream fileStream = new FileStream(GlobalStaticValues.SAVE_PATH + "/debug.json", FileMode.Create)) {
                byte[] info = new UTF8Encoding(true).GetBytes(output);
                fileStream.Write(info, 0, info.Length);
            }
        }

        public static void SaveGame() {
            if(gameData == null) { return; }

            gameData = new GameData(gameData.Name, gameData.Day, Player);

            Debug.Log(Player.Health);

            string output = JsonConvert.SerializeObject(gameData);

            Directory.CreateDirectory(Path.GetDirectoryName(GlobalStaticValues.SAVE_PATH + $"/{gameData.Name}.json"));

            using (FileStream fileStream = new FileStream(GlobalStaticValues.SAVE_PATH + $"/{gameData.Name}.json", FileMode.Create)) {
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
            GameManager.gameData = gameData;
        }

        private static void LoadDataToPlayer(Player player) {
            player.SetHealth(gameData.PlayerData.Health);

            player.MaxHealth = gameData.PlayerData.MaxHealth;
            player.Speed = gameData.PlayerData.Speed;
            player.Defence = gameData.PlayerData.Defence;
            player.Templar = gameData.PlayerData.Templar;
            player.Rogue = gameData.PlayerData.Rogue;
            player.Thaumaturge = gameData.PlayerData.Thaumaturge;
            player.Corruption = gameData.PlayerData.Corruption;

            player.Inventory = new Inventory(Player, gameData.PlayerData.PlayerInventory);

            Player = player;
        }

        public void GoToMainMenu() {
            LoadingManager.Instance.LoadScene(1, GameState.MAINMENU);
        }

        internal static void SpawnPlayer(AsyncOperation _) {
            if (CurrentGameState != GameState.MAINMENU) {
                if (gameData == null) {
                    Debug.LogError("No GameData present!");
                    return;
                }
                PlayerObject = Instantiate(_playerPrefab, new Vector3(0, 1, 0), _playerPrefab.transform.rotation);
                PlayerObject.transform.SetParent(null);
                LoadDataToPlayer(PlayerObject.GetComponent<Player>());
                GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Follow = PlayerObject.transform;
            }
        }
    }

    public enum GameState {
        MAINMENU,
        TOWN,
        DUNGEON
    }
}