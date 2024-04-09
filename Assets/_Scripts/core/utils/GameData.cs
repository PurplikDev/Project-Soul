using Newtonsoft.Json;
using roguelike.environment.entity.player;
using roguelike.environment.entity.statsystem;
using System;

namespace roguelike.core.utils.gamedata {
    [Serializable]
    public class GameData : IComparable<GameData> {
        public string Name;
        public int Day;
        public PlayerData PlayerData;
        public DateTime SaveTime;

        public bool IsPermaDeath;

        public GameData(string name, int day, Player player, bool isPermaDeath) {
            Name = name;
            Day = day;
            PlayerData = new PlayerData(player);
            SaveTime = DateTime.Now;
            IsPermaDeath = isPermaDeath;
        }

        [JsonConstructor]
        public GameData(string name, int day, PlayerData playerData, DateTime time, bool isPermaDeath) {
            Name = name;
            Day = day;
            PlayerData = playerData;
            SaveTime = time;
            IsPermaDeath = isPermaDeath;
        }

        public static GameData EMPTY {
            get {
                return new GameData("", 0, new PlayerData(30, new Stat(30), new Stat(2.5f), new Stat(0), new Stat(0), new Stat(0), new Stat(0), new Stat(0), new item.InventoryData()), DateTime.Now, true);
            }
        }

        public int CompareTo(GameData other) {
            if (SaveTime > other.SaveTime) return -1;
            else return 1;
        }
    }
}