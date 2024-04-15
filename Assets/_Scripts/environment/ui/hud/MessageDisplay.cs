using Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace roguelike.rendering.ui {
    public class MessageDisplay : MonoBehaviour {

        [SerializeField] Text text;

        public void DisplayMessage(string message, float duration) {
            text.text = TranslationManager.GetTranslation(message);
            ShowMessage(duration);
        }

        private void ShowMessage(float duration) {
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
                    SleepMessage(duration);
                }
            };

            gameObject.AddTween(tween);
        }

        private void SleepMessage(float duration) {
            var tween = new FloatTween {
                duration = duration,
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