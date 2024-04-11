using Tweens;
using UnityEngine;

namespace roguelike.environment.world.dungeon {
    public class BossGate : MonoBehaviour {

        [SerializeField] GateState defaultState;
        [SerializeField] GameObject gateObject;

        [SerializeField] Vector3 openPos = new Vector3(0, 5, 0);
        [SerializeField] Vector3 closedPos = new Vector3(0, 0, 0);

        public void Awake() {
            if (defaultState == GateState.OPEN) {
                gateObject.transform.position = openPos;
            } else {
                gateObject.transform.position = closedPos;
            }
        }

        public void Open() {
            GateAction(closedPos, openPos);
        }

        public void Close() {
            GateAction(openPos, closedPos);
        }

        private void GateAction(Vector3 posFrom, Vector3 posTo) {
            var tween = new Vector3Tween {
                duration = 2.5f,
                from = posFrom,
                to = posTo,
                easeType = EaseType.ExpoInOut,
                onUpdate = (_, value) => {
                    gateObject.transform.position = value;
                }
            };

            gameObject.AddTween(tween);
        }
    }

    enum GateState {
        OPEN, CLOSED
    }
}