using Newtonsoft.Json;
using roguelike.core.utils;
using roguelike.core.utils.gamedata;
using roguelike.environment.entity.player;
using roguelike.system.input;
using roguelike.system.singleton;
using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace roguelike.system.manager {
    public class GameManager : Singleton<GameManager> {
        private static bool _isSingleplayer = true; // THIS VALUE IS  NOT AFFECTED RN, I JUST HAVE IT HERE FOR THE FUTURE:TM:
        public static bool IsSinglePlayer { get { return _isSingleplayer; } }

        public Action StartGame;
        public Action GlobalPauseEvent;

        protected GameData gameData {  get; private set; }

        public Player Player { get { return GameObject.Find("Player").GetComponent<Player>(); } }
        public PlayerInput Input { get { return Player.PlayerInput; } }
        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void Start() {
            gameData = new GameData("Purplik", 0, Player);

            string output = JsonConvert.SerializeObject(gameData);

            Debug.Log(output);

            Directory.CreateDirectory(Path.GetDirectoryName(GlobalStaticValues.SAVE_PATH + "/save.json"));

            using (FileStream fileStream = new FileStream(GlobalStaticValues.SAVE_PATH + "/save.json", FileMode.Create)) {
                byte[] info = new UTF8Encoding(true).GetBytes(output);
                fileStream.Write(info, 0, info.Length);
            }
        }

        private void HandlePause()
        {
            if (IsSinglePlayer)
            {
                GlobalPauseEvent.Invoke();
            }
        }

        public enum GameState {
            LOADING,
            MAINMENU,
            TOWN,
            DUNGEON
        }
    }
}