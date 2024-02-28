using roguelike.core.utils;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.mainmenu {
    public class MainMenuRenderer : MonoBehaviour {
        protected UIDocument document;

        private VisualElement _root, _menuRoot, _saveMenuRoot;
        private ListView _saveFileList;

        private VisualTreeAsset _saveFileElement;

        private void Awake() {
            document = GetComponent<UIDocument>();
            _saveFileElement = Resources.Load<VisualTreeAsset>("ui/uidocuments/templates/SaveElement");

            _root = document.rootVisualElement;

            _menuRoot = _root.Q<VisualElement>("MainMenuHolder");
            _saveMenuRoot = _root.Q<VisualElement>("SaveMenuHolder");
            _saveFileList = _saveMenuRoot.Q<ListView>("SaveList");

            foreach(var save in Directory.GetFiles(GlobalStaticValues.SAVE_PATH)) {
                _saveFileList.makeItem = () => {
                    var newListEntry = _saveFileElement.Instantiate();

                    var newSaveElementController = new SaveElementController();

                    newListEntry.userData = newSaveElementController;
                    newSaveElementController.SetupElement(newListEntry);

                    return newListEntry;
                };

                _saveFileList.bindItem = (item, index) => {
                    // (item.userData as SaveElementController).FillData();
                };
            }
        }
    }
}