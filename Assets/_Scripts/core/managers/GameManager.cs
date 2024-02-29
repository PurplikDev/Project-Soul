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
using UnityEngine.SceneManagement;

namespace roguelike.system.manager {
    public class GameManager : Singleton<GameManager> {

        public Action StartGame;
        public Action GlobalPauseEvent;

        protected GameData gameData {  get; private set; }

        public Player Player { get; private set; }
        public PlayerInput Input { get { return Player.PlayerInput; } }



        private void GenerateDebugSave() {
            gameData = new GameData("Purplik", 0, Player);

            string output = JsonConvert.SerializeObject(gameData);

            Debug.Log(output);

            Directory.CreateDirectory(Path.GetDirectoryName(GlobalStaticValues.SAVE_PATH + "/debug.json"));

            using (FileStream fileStream = new FileStream(GlobalStaticValues.SAVE_PATH + "/debug.json", FileMode.Create)) {
                byte[] info = new UTF8Encoding(true).GetBytes(output);
                fileStream.Write(info, 0, info.Length);
            }
        }


        public void CreateNewGame() {

        }

        public void LoadGame(GameData gameData) {

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            var playerObject = Resources.Load<GameObject>("prefabs/entities/player");

            Player = playerObject.GetComponent<Player>();

            Player.MaxHealth = gameData.PlayerData.MaxHealth;
            Player.Speed = gameData.PlayerData.Speed;
            Player.Defence = gameData.PlayerData.Defence;
            Player.Templar = gameData.PlayerData.Templar;
            Player.Rogue = gameData.PlayerData.Rogue;
            Player.Thaumaturge = gameData.PlayerData.Thaumaturge;
            Player.Corruption = gameData.PlayerData.Corruption;

            Instantiate(playerObject);

            SceneManager.UnloadSceneAsync(0);
        }
    }
}