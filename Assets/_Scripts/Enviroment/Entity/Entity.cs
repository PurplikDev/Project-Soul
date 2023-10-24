using roguelike.enviroment.entity.StatSystem;
using UnityEngine;

namespace roguelike.enviroment.entity {
    public class Entity : MonoBehaviour {
        public Stat Health;
        public Stat Speed;
        public Stat Defence;

        public Stat Templar;
        public Stat Rogue;
        public Stat Thaumaturge;



        public Vector3 Position { get { return transform.position; } }
        public Quaternion Rotation { get { return transform.rotation; } }
    }
}