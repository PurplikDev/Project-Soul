using roguelike.environment.entity.player;
using roguelike.rendering.ui.dungeonboard;
using Tweens;
using UnityEngine;

namespace roguelike.environment.world {
    public class Elevator : MonoBehaviour {

        public Direction ElevatorDirection;
        public GameObject ElevatorObject;

        private void OnTriggerEnter(Collider other) {
            var player = other.GetComponent<Player>();

            if(player != null) {
                var tween = new PositionYTween {
                    duration = 5f,
                    easeType = EaseType.ExpoIn,
                    from = ElevatorObject.transform.position.y,
                    to = ElevatorObject.transform.position.y - 5,
                    onStart = (_) => {
                        player.transform.SetParent(ElevatorObject.transform);
                    },
                    onUpdate = (_, value) => {
                        ElevatorObject.transform.position = ElevatorObject.transform.position + new Vector3(0, value, 0);
                    },
                    onFinally = (_) => {
                        DungeonBoardRenderer.Instance.DisplayBoard();
                    }
                };

                gameObject.AddTween(tween);
            }
        }

        public enum Direction { UP, DOWN }
    }
}