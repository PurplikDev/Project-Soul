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
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace roguelike.system.manager {
    public class GameManager : Singleton<GameManager> {

        public static GameState CurrentGameState;

        public Action StartGame;
        public Action GlobalPauseEvent;

        protected static GameData gameData { get; private set; }
        public static Player Player { get; private set; }
        public static GameObject PlayerObject { get; private set; }
        public static PlayerInput Input { get { return Player.PlayerInput; } }

        public virtual void Start() {
            if (CurrentGameState != GameState.MAINMENU) {
                if (gameData == null) {
                    Debug.LogError("No GameData present!");
                    return;
                }
                GameObject player = Instantiate(PlayerObject, new Vector3(0, 1, 0), PlayerObject.transform.rotation);
                GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
            }
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

            PlayerObject = Resources.Load<GameObject>("prefabs/entities/player");
            Player = PlayerObject.GetComponent<Player>();

            Player.SetHealth(gameData.PlayerData.Health);

            Player.MaxHealth = gameData.PlayerData.MaxHealth;
            Player.Speed = gameData.PlayerData.Speed;
            Player.Defence = gameData.PlayerData.Defence;
            Player.Templar = gameData.PlayerData.Templar;
            Player.Rogue = gameData.PlayerData.Rogue;
            Player.Thaumaturge = gameData.PlayerData.Thaumaturge;
            Player.Corruption = gameData.PlayerData.Corruption;

            Player.Inventory = new Inventory(Player, gameData.PlayerData.PlayerInventory);
        }

        public void GoToMainMenu() {
            LoadingManager.Instance.LoadScene(0, GameState.MAINMENU);
        }
    }

    public enum GameState {
        MAINMENU,
        TOWN,
        DUNGEON
    }
}