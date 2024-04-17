using Newtonsoft.Json;
using roguelike.core.utils;
using roguelike.system.manager;
using roguelike.system.singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TranslationManager : PersistentSingleton<TranslationManager>{
    public static Language Lang;
    public static Dictionary<string, string> language = new Dictionary<string, string>();

    private void Start() {
        Lang = GameManager.CurrentGameSettings.Lang;
        GetTranslationFromFile();
        GameSettings.GameSettingsChanged += UpdatePickedLanguage;
    }

    public static string GetTranslation(string key) {
        try {
            return language[key];
        } catch (KeyNotFoundException) {
            Debug.LogError("[" + key + "] isn't present in the lang file. Adding a placeholder into the cache!");
            language.Add(key, key);
            return key;
        }
    }

    public static void GetTranslationFromFile() {
        TextAsset textAsset = Resources.Load<TextAsset>("data/lang/" + Lang.ToString());
        TextAsset defaultAsset = Resources.Load<TextAsset>("data/lang/" + Language.en_us.ToString());
        if (textAsset == null) { 
            Debug.LogError("Your lang file is empty! Selecting a default lang file!");
            textAsset = defaultAsset;
        }

        language = JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.text.ToString());
        
        var englishLang  = JsonConvert.DeserializeObject<Dictionary<string, string>>(defaultAsset.text.ToString());

        foreach(var pair in englishLang) {
            if(!language.ContainsKey(pair.Key)) {
                language.Add(pair.Key, pair.Value);
            }
        }

    }

    public static void TranslateHeader(Label label) {
        label.text = GetTranslation(label.text);
    }

    public static void TranslateHeader(Label label, string entry) {
        label.text = GetTranslation(entry);
    }

    public static void UpdatePickedLanguage(GameSettings settings) {
        Lang = settings.Lang;
        GetTranslationFromFile();
    }

    public enum Language {
        en_us,
        en_pt
    }
}
