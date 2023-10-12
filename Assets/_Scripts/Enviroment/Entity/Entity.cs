using roguelike.enviroment.entity.StatSystem;
using UnityEngine;

namespace roguelike.enviroment.entity {
    public class Entity : MonoBehaviour {
        [Header("<color=#80FF75>Stats")]
        public Stat Health;
        public Stat Speed;
        public Stat Defence;
        [Space(4)]
        [Header("<color=#80FF75>Class Stats")]
        public Stat Templar;
        public Stat Rogue;
        public Stat Thaumaturge;



        public Vector3 Position { get { return transform.position; } }
        public Quaternion Rotation { get { return transform.rotation; } }
    }
}