using roguelike.system.input;
using roguelike.system.singleton;
using System;
using UnityEngine;

namespace roguelike.system.gamemanager {
    public class GameManager : Singleton<GameManager> {
        private static bool _isSingleplayer = true; // THIS VALUE IS  NOT AFFECTED RN, I JUST HAVE IT HERE FOR THE FUTURE:TM:
        public static bool IsSinglePlayer { get { return _isSingleplayer; } }

        public Action StartGame;

        private void Start() {
            StartGame += spawnPlayer;
            StartGame.Invoke();
        }

        private void spawnPlayer() {

        }

        public enum GameState {
            LOADING,
            MAINMENU,
            TOWN,
            DUNGEON
        }
    }
}