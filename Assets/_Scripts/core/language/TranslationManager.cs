using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public static class TranslationManager {

    public static Language lang = Language.en_us;
    public static Dictionary<string, string> language = new Dictionary<string, string>();

    public static string getTranslation(string key) {
        return language[key];
    }

    // todo: event that calls this method when player changes their language or start the game
    public static void getTranslationFromFile() { 
        TextAsset textAsset = Resources.Load<TextAsset>("lang/" + lang.ToString());

        if (textAsset == null) { 
            Debug.LogError("Your lang file is empty! Selecting a default lang file!");
            textAsset = Resources.Load<TextAsset>("lang/" + Language.en_us.ToString());
        }

        language = JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.text.ToString());
    }

    public enum Language {
        en_us,
        en_pt
    }
}
