using roguelike.environment.entity.player;
using roguelike.system.manager;
using Tweens;
using UnityEngine;

namespace roguelike.environment.world.interactable {
    public class Gate : MonoBehaviour, IHoverable {
        
        public GateType Type;

        bool isOpening;
        Vector3 defaultPosition;

        private void Awake() {
            defaultPosition = transform.position;
        }

        public void Interact(Player player) {
            if(isOpening) { return; }

            var tween = new PositionYTween {
                duration = 2.5f,
                from = transform.position.y,
                to = defaultPosition.y + 2.5f,
                easeType = EaseType.ExpoOut,
                onStart = (_) => {
                    isOpening = true;
                },
                onUpdate = (_, value) => {
                    transform.position = new Vector3(defaultPosition.x, defaultPosition.y + value, defaultPosition.z);
                },
                onFinally = (_) => {
                    if (Type == GateType.LAYER_EXIT) {
                        DungeonManager.Instance.ExitLayer();
                    } else {
                        DungeonManager.Instance.ExitCamp();
                    }

                    isOpening = false;
                }
            };
            gameObject.AddTween(tween);
        }

        public void OnHover(Player player) { }
        public void OnHoverEnter(Player player) {
            if(isOpening) { return; }
            var tween = new PositionYTween {
                duration = 1.25f,
                from = transform.position.y,
                to = defaultPosition.y + 0.5f,
                easeType = EaseType.ExpoOut,
                onUpdate = (_, value) => {
                    transform.position = new Vector3(defaultPosition.x, defaultPosition.y + value, defaultPosition.z);
                }
            };
            gameObject.AddTween(tween);
        }

        public void OnHoverExit(Player player) {
            if (isOpening) { return; }
            var tween = new PositionYTween {
                duration = 1.25f,
                from = transform.position.y,
                to = defaultPosition.y,
                easeType = EaseType.ExpoOut,
                onUpdate = (_, value) => {
                    transform.position = new Vector3(defaultPosition.x, defaultPosition.y + value, defaultPosition.z);
                }
            };
            gameObject.AddTween(tween);
        }
    }

    public enum GateType {
        LAYER_EXIT,
        CAMP_EXIT
    }
}
