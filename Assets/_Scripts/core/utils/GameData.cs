using Newtonsoft.Json;
using roguelike.environment.entity.player;
using roguelike.environment.entity.statsystem;

namespace roguelike.core.utils.gamedata {
    [System.Serializable]
    public class GameData {
        public string Name;
        public int Day;
        public PlayerData PlayerData;

        public GameData(string name, int day, Player player) {
            Name = name;
            Day = day;
            PlayerData = new PlayerData(player);
        }

        [JsonConstructor]
        public GameData(string name, int day, PlayerData playerData) {
            Name = name;
            Day = day;
            PlayerData = playerData;
        }

        public static GameData EMPTY {
            get {
                return new GameData("", 0, new PlayerData(30, new Stat(30), new Stat(2.5f), new Stat(0), new Stat(0), new Stat(0), new Stat(0), new Stat(0), new item.InventoryData()));
            }
        }
    }
}