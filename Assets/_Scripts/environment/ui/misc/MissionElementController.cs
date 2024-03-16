using roguelike.system.manager;
using UnityEngine.UIElements;
using static roguelike.system.manager.DungeonManager;

namespace roguelike.rendering.ui.dungeonboard {
    public class MissionElementController {
        Label missionName, missionDifficulty;
        VisualElement missionIcon;

        DungeonDifficulty difficulty;

        DungeonBoardRenderer dungeonBoardRenderer;

        public void SetupElement(VisualElement root, DungeonBoardRenderer renderer) {
            missionName = root.Q<Label>("MissionHeader");
            missionDifficulty = root.Q<Label>("DifficultyHeader");
            missionIcon = root.Q<VisualElement>("MissionIcon");
            root.RegisterCallback<PointerDownEvent>(OnClicked);
            dungeonBoardRenderer = renderer;
        }

        public void FillData(DungeonDifficulty dungeonDifficulty) {
            missionName.text = "mission"; // random name pool to pick
            missionIcon.style.backgroundImage = null; // add random image pool to pick
            missionDifficulty.text = TranslationManager.GetTranslation($"dungeon.{dungeonDifficulty.ToString().ToLower()}");
            difficulty = dungeonDifficulty;
        }

        public void OnClicked(PointerDownEvent evt) {
            DungeonManager.Instance.EnterDungeon(difficulty);
        }
    }
}