using System.Collections.Generic;
using roguelike.enviroment.entity.statsystem;
using UnityEngine;
using static roguelike.enviroment.entity.statsystem.Stat;

namespace roguelike.enviroment.entity {
    public class Entity : MonoBehaviour {
        public Stat MaxHealth = new Stat(100);
        public Stat Speed = new Stat(5);
        public Stat Defence = new Stat(0);

        public Stat Templar = new Stat(0);
        public Stat Rogue = new Stat(0);
        public Stat Thaumaturge = new Stat(0);

        public Dictionary<StatType, Stat> StatByType = new Dictionary<StatType, Stat>();

        public Vector3 Position { get { return transform.position; } }
        public Quaternion Rotation { get { return transform.rotation; } }
        public Quaternion LookDirection;

        protected virtual void Awake() {
            StatByType.Add(StatType.HEALTH, MaxHealth);
            StatByType.Add(StatType.SPEED, Speed);
            StatByType.Add(StatType.DEFENCE, Defence);
            StatByType.Add(StatType.TEMPLAR, Templar);
            StatByType.Add(StatType.ROGUE, Rogue);
            StatByType.Add(StatType.THAUMATURGE, Thaumaturge);
        }

        protected virtual void Update() { }

        public virtual void PrimaryAction() {
            Debug.Log("Entity primary action!");
        }

        public virtual void SecondaryAction() {
            Debug.Log("Entity secondary action!");
        }

        public virtual void Damage(DamageSource source) {

        }

        public int GetDefenceTier() {
            return Mathf.FloorToInt(Defence.Value);
        }

        public enum DamageSource {
            COMBAT,            
            CORRUPTION,
            ENVIROMENT
        }

        public enum DamageType {
            NORMAL,
            TRUE
        }
    }
}