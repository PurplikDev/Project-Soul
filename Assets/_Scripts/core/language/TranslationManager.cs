using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class TranslationManager {

    public static Language lang = Language.en_us;
    public static Dictionary<string, string> language = new Dictionary<string, string>();

    static TranslationManager() {
        getTranslationFromFile();
    }

    public static string getTranslation(string key) {
        try {
            return language[key];
        } catch (KeyNotFoundException) {
            Debug.LogWarning("[" + key + "] isn't present in the lang file. Adding a placeholder into the cache!");
            language.Add(key, key);
            return key;
        }
    }

    // todo: event that calls this method when player changes their language or start the game
    public static void getTranslationFromFile() { 
        TextAsset textAsset = Resources.Load<TextAsset>("data/lang/" + lang.ToString());

        if (textAsset == null) { 
            Debug.LogError("Your lang file is empty! Selecting a default lang file!");
            textAsset = Resources.Load<TextAsset>("data/lang/" + Language.en_us.ToString());
        }

        language = JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.text.ToString());
    }

    public static void TranslateHeader(Label label) {
        label.text = getTranslation(label.text);
    }

    public enum Language {
        en_us,
        en_pt
    }
}
