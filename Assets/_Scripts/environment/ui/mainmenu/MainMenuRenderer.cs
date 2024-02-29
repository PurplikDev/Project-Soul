using roguelike.core.utils;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.mainmenu {
    public class MainMenuRenderer : MonoBehaviour {
        protected UIDocument document;

        private VisualElement _root, _menuRoot, _saveMenuRoot;
        private ScrollView _saveFileList;

        private VisualTreeAsset _saveFileElement;

        private void Awake() {
            document = GetComponent<UIDocument>();
            _saveFileElement = Resources.Load<VisualTreeAsset>("ui/uidocuments/templates/SaveElement");

            _root = document.rootVisualElement;

            TranslateHeader(_root.Q<Label>("SavesHeader"));
            TranslateHeader(_root.Q<Label>("NewSaveButtonHeader"));

            _menuRoot = _root.Q<VisualElement>("MainMenuHolder");
            _saveMenuRoot = _root.Q<VisualElement>("SaveMenuHolder");
            _saveFileList = _saveMenuRoot.Q<ScrollView>("SaveList");

            foreach(var save in Directory.GetFiles(GlobalStaticValues.SAVE_PATH)) {
                var newSaveElement = _saveFileElement.Instantiate();
                var newSaveElementController = new SaveElementController();
                newSaveElement.userData = newSaveElementController;
                newSaveElementController.SetupElement(newSaveElement);
                _saveFileList.Add(newSaveElement);
            }
        }

        protected void TranslateHeader(Label label) {
            label.text = TranslationManager.getTranslation(label.text);
        }
    }
}