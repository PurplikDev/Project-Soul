using UnityEngine;

namespace roguelike.core.utils {
    public class GlobalStaticValues {
        public static readonly string EMPTY_STACK_WARNING = "You are trying to interact with an empty slot!";

        public static readonly string SHAPED_RECIPE_SIZE_ERROR = "Shaped recipe isn't the correct size!";

        public static readonly string SAVE_PATH = Application.persistentDataPath + "/GameSaves";
    }
}