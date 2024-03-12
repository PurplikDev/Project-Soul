using roguelike.environment.entity.player;
using Tweens;
using UnityEngine;

namespace roguelike.environment.world.interactable {
    public class Door : MonoBehaviour, IHoverable {

        public bool NeedsKey;
        bool isOpen, isOpening = false;

        Quaternion defaultRotation;

        private void Awake() {
            defaultRotation = transform.rotation;
        }

        public void Interact(Player player) {
            DoorInteraction();
        }

        public void OnHover(Player player) {}

        public void OnHoverEnter(Player player) {
            if(isOpen || isOpening) { return; }
            var tween = new QuaternionTween {
                duration = 0.25f,
                from = transform.rotation,
                to = Quaternion.Euler(0, defaultRotation.eulerAngles.y - 20, 0),
                easeType = EaseType.ExpoInOut,
                onUpdate = (_, value) => {
                    transform.rotation = value;
                }
            };
            gameObject.AddTween(tween);
        }

        public void OnHoverExit(Player player) {
            if (isOpen || isOpening) { return; }
            var tween = new QuaternionTween {
                duration = 0.25f,
                from = transform.rotation,
                to = defaultRotation,
                easeType = EaseType.ExpoInOut,
                onUpdate = (_, value) => {
                    transform.rotation = value;
                }
            };
            gameObject.AddTween(tween);
        }

        private void DoorInteraction() {
            if(NeedsKey) {
                Debug.LogError("ADD KEY LOGIC!!!!");
            }
            
            var collider = GetComponent<BoxCollider>();

            var tween = new QuaternionTween {
                duration = 0.5f,
                from = transform.rotation,
                to = isOpen ? defaultRotation : Quaternion.Euler(0, defaultRotation.eulerAngles.y - 105, 0),
                easeType = EaseType.ExpoInOut,
                onStart = (_) => {
                    collider.enabled = false;
                    isOpening = true;
                },
                onUpdate = (_, value) => {
                    transform.rotation = value;
                },
                onFinally = (_) => {
                    collider.enabled = true;
                    isOpen = !isOpen;
                    isOpening = false;
                }
            };
            
            gameObject.AddTween(tween);
        }
    }
}