using roguelike.system.manager;
using Tweens;
using UnityEngine;

namespace roguelike.core.utils.prefab {
    public class WallPrefab : MonoBehaviour {
        private Renderer _renderer;

        private bool _isVisible, _isHiding = false;

        private Vector3 _defaultPosition;

        private void Awake() {
            _renderer = GetComponentInChildren<Renderer>();
            _defaultPosition = _renderer.transform.position;
        }
        
        private void Update() {
            if (!_renderer.isVisible) { return; }

            Vector3 direction = (GameManager.PlayerObject.transform.position - Camera.main.transform.position).normalized;
            Ray ray = new Ray(Camera.main.transform.position, direction);
            if (Physics.Raycast(
                    ray,
                    out var hit,
                    Vector3.Distance(Camera.main.transform.position, GameManager.PlayerObject.transform.position),
                    LayerMask.GetMask("Wall"),
                    QueryTriggerInteraction.Ignore)) {

                if (hit.transform == transform) {
                    if (!_isHiding && hit.point.y > 5) {
                        _isHiding = true;
                        Hide(_defaultPosition.y - 10);
                    }
                } else if (_isHiding) {
                    _isHiding = false;
                    Hide(_defaultPosition.y);
                }
            }
        }


        private void Hide(float position) {
            var tween = new FloatTween {
                duration = 1,
                from = _renderer.transform.position.y,
                to = position,
                onUpdate = (_, value) => {
                    _renderer.transform.position = new Vector3(_defaultPosition.x, value, _defaultPosition.z);
                }
            };

            gameObject.AddTween(tween);
        }
    }
}