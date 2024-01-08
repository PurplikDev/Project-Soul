using roguelike.environment.entity.player;
using roguelike.system.input;
using roguelike.system.singleton;
using System;
using UnityEngine;

namespace roguelike.system.manager {
    public class GameManager : Singleton<GameManager> {
        private static bool _isSingleplayer = true; // THIS VALUE IS  NOT AFFECTED RN, I JUST HAVE IT HERE FOR THE FUTURE:TM:
        public static bool IsSinglePlayer { get { return _isSingleplayer; } }

        public Action StartGame;
        public Action GlobalPauseEvent;

        public Player Player { get { return GameObject.Find("Player").GetComponent<Player>(); } }
        public PlayerInput Input { get { return Player.PlayerInput; } }

        protected override void Awake()
        {

            StartGame += SpawnPlayer;
            StartGame.Invoke();

            // if the game is in singleplayer it will actually pause stuff in game
            // but when ur in multiplayer the game won't pause (for example entities will still move and stuff)
            

            base.Awake();
        }

        protected virtual void Start() {
            
        }

        private void SpawnPlayer() {
            
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