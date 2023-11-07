using System.Collections.Generic;
using roguelike.enviroment.entity.StatSystem;
using UnityEngine;
using static roguelike.enviroment.entity.StatSystem.Stat;

namespace roguelike.enviroment.entity {
    public class Entity : MonoBehaviour {
        public Stat Health = new Stat(100);
        public Stat Speed = new Stat(10);
        public Stat Defence = new Stat(0);

        public Stat Templar = new Stat(0);
        public Stat Rogue = new Stat(0);
        public Stat Thaumaturge = new Stat(0);

        public Dictionary<StatType, Stat> StatByType = new Dictionary<StatType, Stat>();

        public Vector3 Position { get { return transform.position; } }
        public Quaternion Rotation { get { return transform.rotation; } }

        protected virtual void Awake() {
            StatByType.Add(StatType.HEALTH, Health);
            StatByType.Add(StatType.SPEED, Speed);
            StatByType.Add(StatType.DEFENCE, Defence);
            StatByType.Add(StatType.TEMPLAR, Templar);
            StatByType.Add(StatType.ROGUE, Rogue);
            StatByType.Add(StatType.THAUMATURGE, Thaumaturge);
        }

        protected virtual void Update() {

        }

        public void ApplyStatModifier(StatModifier modifier) {

        }
    }
}