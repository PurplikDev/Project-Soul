using Newtonsoft.Json;
using roguelike.core.utils.gamedata;
using System.IO;

namespace roguelike.core.utils {
    public class SaveFileUtils {
        public static GameData GetDataFromFile(string path) {
            return JsonConvert.DeserializeObject<GameData>(File.ReadAllText(@path));
        }
    }
}