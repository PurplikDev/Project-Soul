using roguelike.core.utils.gamedata;
using roguelike.system.manager;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.mainmenu {
    public class SaveElementController {

        Label saveName, dayCount, dateDisplay;

        GameData gameData;

        MainMenuRenderer mainMenuRenderer;

        public void SetupElement(VisualElement root, MainMenuRenderer renderer) {
            saveName = root.Q<Label>("SaveNameHeader");
            dayCount = root.Q<Label>("SaveDayHeader");
            dateDisplay = root.Q<Label>("SaveDateHeader");
            root.RegisterCallback<PointerDownEvent>(OnClicked);
            mainMenuRenderer = renderer;
        }

        public void FillData(GameData gameData) {
            saveName.text = gameData.Name;
            dayCount.text = TranslationManager.GetTranslation("ui.day") + " " + gameData.Day.ToString();
            dateDisplay.text = gameData.SaveTime.ToString();
            this.gameData = gameData;
        }

        public void OnClicked(PointerDownEvent evt) {
            mainMenuRenderer.StopMusic();
            GameManager.Instance.LoadSave(gameData);
            LoadingManager.Instance.LoadScene(2, GameState.TOWN);
        }
    }
}