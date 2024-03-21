using roguelike.environment.entity.player;
using roguelike.rendering.ui.dungeonboard;
using roguelike.system.manager;
using Tweens;
using UnityEngine;

namespace roguelike.environment.world {
    public class Elevator : MonoBehaviour {
        public Direction ElevatorDirection;
        public ElevatorType Type;
        [Space]
        public GameObject ElevatorObject;

        private void OnTriggerEnter(Collider other) {
            var player = other.GetComponent<Player>();

            if(player != null) {
                var tween = new FloatTween {
                    duration = 7.5f,
                    easeType = EaseType.ExpoInOut,
                    from = ElevatorObject.transform.position.y,
                    to = ElevatorDirection == Direction.DOWN ? ElevatorObject.transform.position.y - 15 : ElevatorObject.transform.position.y + 15,
                    onStart = (_) => {
                        player.transform.SetParent(ElevatorObject.transform);
                        GetComponent<BoxCollider>().enabled = false;
                        GameManager.Input.CharacterControls.Disable();
                    },
                    onUpdate = (_, value) => {
                        ElevatorObject.transform.position = transform.position + new Vector3(0, value, 0);
                    },
                    onFinally = (_) => {
                        switch(Type) {
                            case ElevatorType.TOWN_EXIT:
                                DungeonBoardRenderer.Instance.DisplayBoard();
                                break;

                            case ElevatorType.DUNGEON_EXIT:
                                DungeonManager.Instance.ExitDungeon();
                                break;

                            default:
                                GameManager.Input.CharacterControls.Enable();
                                break;
                        }
                    }
                };

                gameObject.AddTween(tween);
            }
        }

        private void OnTriggerExit(Collider other) {
            var player = other.GetComponent<Player>();

            if (player != null) {
                GetComponent<BoxCollider>().enabled = true;
            }
        }

        public enum Direction { UP, DOWN }
        public enum ElevatorType { DUNGEON_ENTRY, DUNGEON_EXIT, TOWN_EXIT }
    }
}