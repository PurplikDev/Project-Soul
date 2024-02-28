using roguelike.core.utils.gamedata;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.mainmenu {
    public class SaveElement : VisualElement {
        public Label SaveTitle, SaveDays;

        public SaveElement(GameData gameData) {
            SaveTitle = new Label();
            SaveTitle.text = gameData.Name;
        }
    }
}