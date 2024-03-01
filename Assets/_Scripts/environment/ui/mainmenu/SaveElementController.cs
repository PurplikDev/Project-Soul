using roguelike.core.utils.gamedata;
using roguelike.system.manager;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.mainmenu {
    public class SaveElementController {

        Label saveName;
        Label dayCount;

        GameData gameData;

        public void SetupElement(VisualElement root) {
            saveName = root.Q<Label>("SaveNameHeader");
            dayCount = root.Q<Label>("SaveDayHeader");
            root.RegisterCallback<PointerDownEvent>(OnClicked);
        }

        public void FillData(GameData gameData) {
            saveName.text = gameData.Name;
            dayCount.text = gameData.Day.ToString();
            this.gameData = gameData;
        }

        public void OnClicked(PointerDownEvent evt) {
            GameManager.Instance.LoadSave(gameData);
            GameManager.Instance.LoadGame(1, GameState.TOWN);
        }
    }
}