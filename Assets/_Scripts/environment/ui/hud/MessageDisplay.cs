using Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace roguelike.rendering.ui {
    public class MessageDisplay : MonoBehaviour {

        [SerializeField] Text text;

        public void DisplayMessage(string message) {
            text.text = TranslationManager.GetTranslation(message);
            ShowMessage();
        }

        private void ShowMessage() {
            var color = text.color;

            var tween = new FloatTween {
                duration = 0.5f,
                from = 0f,
                to = 1f,
                onUpdate = (_, value) => {
                    color.a = value;
                    text.color = color;
                },
                onFinally = (_) => {
                    SleepMessage();
                }
            };

            gameObject.AddTween(tween);
        }

        private void SleepMessage() {
            var tween = new FloatTween {
                duration = 2.5f,
                onFinally = (_) => {
                    HideMessage();
                }
            };

            gameObject.AddTween(tween);
        }

        private void HideMessage() {
            var color = text.color;

            var tween = new FloatTween {
                duration = 0.5f,
                from = 1f,
                to = 0f,
                onUpdate = (_, value) => {
                    color.a = value;
                    text.color = color;
                }
            };

            gameObject.AddTween(tween);
        }
    }
}