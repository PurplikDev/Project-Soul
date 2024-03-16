using roguelike.core.utils.mathematicus;
using UnityEngine;
using UnityEngine.UIElements;
using static roguelike.system.manager.DungeonManager;

namespace roguelike.rendering.ui.dungeonboard {
    public class DungeonBoardRenderer : MonoBehaviour {

        private VisualTreeAsset _missionOffer { get { return Resources.Load<VisualTreeAsset>("ui/uidocuments/templates/MissionOffer"); } }

        private VisualElement _root, _offerHolder;

        private void Awake() {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _offerHolder = _root.Q<VisualElement>("DungeonOfferHolder");

            TranslationManager.TranslateHeader(_root.Q<Label>("DungeonBoardHeader"));

            for(int i = 0; i< 3; i++) {
                if(Mathematicus.ChanceIn(1, 2)) { CreateOffer(DungeonDifficulty.EASY); } 
                else if (Mathematicus.ChanceIn(1, 3)) { CreateOffer(DungeonDifficulty.NORMAL); }
                else { CreateOffer(DungeonDifficulty.HARD);}
            }
        }

        private void CreateOffer(DungeonDifficulty difficulty) {
            var offer = _missionOffer.Instantiate();
            var missionElementController = new MissionElementController();
            offer.userData = missionElementController;
            missionElementController.SetupElement(offer, this);
            missionElementController.FillData(difficulty);
            _offerHolder.Add(offer);
        }
    }
}