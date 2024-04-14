using UnityEngine;

namespace roguelike.core.utils {
    public class LookAtCameraObject : MonoBehaviour {
        private void Update() {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}