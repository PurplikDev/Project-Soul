using roguelike.system.manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static roguelike.system.manager.DungeonManager;

namespace roguelike.rendering.ui.dungeonboard {
    public class MissionElementController {
        Label missionName, missionDifficulty;
        VisualElement missionIcon;

        DungeonDifficulty difficulty;

        DungeonBoardRenderer dungeonBoardRenderer;

        List<string> missionNames = new List<string>() { "Ancient Halls", "Soulless Corridors", "John Waller's home", "Local Dorms" };

        public void SetupElement(VisualElement root, DungeonBoardRenderer renderer) {
            missionName = root.Q<Label>("MissionHeader");
            missionDifficulty = root.Q<Label>("DifficultyHeader");
            missionIcon = root.Q<VisualElement>("MissionIcon");
            root.RegisterCallback<PointerDownEvent>(OnClicked);
            dungeonBoardRenderer = renderer;
        }

        public void FillData(DungeonDifficulty dungeonDifficulty) {
            missionName.text = missionNames[Random.Range(0, missionNames.Count)];

            var images = Resources.LoadAll<Sprite>("sprites/ui/missions");

            missionIcon.style.backgroundImage = images[Random.Range(0, images.Length)].texture;
            missionDifficulty.text = TranslationManager.GetTranslation($"dungeon.{dungeonDifficulty.ToString().ToLower()}");
            difficulty = dungeonDifficulty;
        }

        public void OnClicked(PointerDownEvent evt) {
            DungeonManager.State = DungeonState.DUNGEON;
            DungeonManager.Instance.EnterDungeon(difficulty);
        }
    }
}