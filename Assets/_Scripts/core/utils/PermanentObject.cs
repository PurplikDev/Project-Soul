using Unity.Properties;
using UnityEngine;

namespace roguelike.core.utils {

    // this is literally just an empty object
    // it does NOTHING except that it stays in the scene
    // why? i like to keep things organized

    public class PermanentObject : MonoBehaviour { private void Awake() { DontDestroyOnLoad(this); } }
}