using System.Collections;
using System.Text;
using roguelike.core.utils.dialogutils;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.ui.dialog {
    public class DialogScreen : MonoBehaviour {

        private VisualElement _rootElement;
        private VisualElement _dialogPortrait;
        private Label _dialogNametag;
        private Label _dialogText;

        public UIDocument DialogDocument { get; private set; }

        private void Awake() {
            DialogDocument = GetComponent<UIDocument>();

            _rootElement = DialogDocument.rootVisualElement;

            _dialogPortrait = _rootElement.Q<VisualElement>("DialogPortrait");
            _dialogNametag = _rootElement.Q<Label>("DialogNametag");
            _dialogText = _rootElement.Q<Label>("DialogText");
        }

        public void Display(Sprite portrait, string displayName, string text) {
            _dialogPortrait.style.backgroundImage = portrait.texture;
            _dialogNametag.text = displayName;
            StartCoroutine(DisplayText(text, 0.05f));
        }

        public void Display(DialogData data) {
            _dialogPortrait.style.backgroundImage = data.DialogPortrait.texture;
            _dialogNametag.text = data.DialogNametag;
            StartCoroutine(DisplayText(data.DialogText, 0.05f));
        }

        private IEnumerator DisplayText(string text, float pause) {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < text.Length; i++) {
                sb.Append(text[i]);
                _dialogText.text = sb.ToString();
                var pauseTime = char.IsWhiteSpace(text[i]) ? pause * 4 : pause;
                yield return new WaitForSeconds(pauseTime);
            }
        }
    }
}

