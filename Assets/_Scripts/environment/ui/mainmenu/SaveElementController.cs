using roguelike.core.utils.gamedata;
using roguelike.system.manager;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.mainmenu {
    public class SaveElementController {

        Label saveName, dayCount, dateDisplay;

        GameData gameData;

        public void SetupElement(VisualElement root) {
            saveName = root.Q<Label>("SaveNameHeader");
            dayCount = root.Q<Label>("SaveDayHeader");
            dateDisplay = root.Q<Label>("SaveDateHeader");
            root.RegisterCallback<PointerDownEvent>(OnClicked);
        }

        public void FillData(GameData gameData) {
            saveName.text = gameData.Name;
            dayCount.text = TranslationManager.getTranslation("ui.day") + " " + gameData.Day.ToString();
            dateDisplay.text = gameData.SaveTime.ToString();
            this.gameData = gameData;
        }

        public void OnClicked(PointerDownEvent evt) {
            GameManager.Instance.LoadSave(gameData);
            LoadingManager.Instance.LoadScene(2, GameState.TOWN);
        }
    }
}