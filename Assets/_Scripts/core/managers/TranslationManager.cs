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

    protected override void Awake() {
        GetTranslationFromFile();
        GameSettings.GameSettingsChanged += UpdatePickedLanguage;
        base.Awake();
    }

    public static string GetTranslation(string key) {
        try {
            return language[key];
        } catch (KeyNotFoundException) {
            Debug.LogWarning("[" + key + "] isn't present in the lang file. Adding a placeholder into the cache!");
            language.Add(key, key);
            return key;
        }
    }

    public static void GetTranslationFromFile() {
        TextAsset textAsset = Resources.Load<TextAsset>("data/lang/" + Lang.ToString());

        if (textAsset == null) { 
            Debug.LogError("Your lang file is empty! Selecting a default lang file!");
            textAsset = Resources.Load<TextAsset>("data/lang/" + Language.en_us.ToString());
        }

        language = JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.text.ToString());
    }

    public static void TranslateHeader(Label label) {
        label.text = GetTranslation(label.text);
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
