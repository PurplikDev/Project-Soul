using roguelike.core.utils.gamedata;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.mainmenu {
    public class SaveElementController {

        Label saveName;
        Label dayCount;

        public void SetupElement(VisualElement root) {
            saveName = root.Q<Label>("SaveNameHeader");
            dayCount = root.Q<Label>("SaveDayHeader");
        }

        public void FillData(GameData gameData) {
            saveName.text = gameData.Name;
            dayCount.text = gameData.Day.ToString();
        }
    }
}