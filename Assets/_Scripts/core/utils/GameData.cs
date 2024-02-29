using Newtonsoft.Json;
using roguelike.environment.entity.player;

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
    }
}