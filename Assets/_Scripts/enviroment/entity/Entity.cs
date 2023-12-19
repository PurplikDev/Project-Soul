using System.Collections.Generic;
using roguelike.enviroment.entity.combat;
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

        public List<StatusEffect> ActiveEffects = new List<StatusEffect>();

        public Vector3 Position { get { return transform.position; } }
        public Quaternion Rotation { get { return transform.rotation; } }
        public Vector3 LookDirection;

        public bool Immortal { get; protected set; } = false;
        public bool IsBlocking { get; internal set; } = false;
        public float Health { get; protected set; }

        protected virtual void Awake() {
            StatByType.Add(StatType.HEALTH, MaxHealth);
            StatByType.Add(StatType.SPEED, Speed);
            StatByType.Add(StatType.DEFENCE, Defence);
            StatByType.Add(StatType.TEMPLAR, Templar);
            StatByType.Add(StatType.ROGUE, Rogue);
            StatByType.Add(StatType.THAUMATURGE, Thaumaturge);

            Health = MaxHealth.Value;
        }

        protected virtual void Start() {
            InvokeRepeating(nameof(CheckAndApplyStatusEffects), 1f, 1f);
        }

        protected virtual void Update() { }

        public virtual void PrimaryAction() {
            Debug.Log("Entity primary action!");
        }

        public virtual void SecondaryAction() {
            Debug.Log("Entity secondary action!");
        }

        public int GetDefenceTier() {
            return Mathf.FloorToInt(Defence.Value);
        }

        public void Damage(DamageSource source) {
            // check for blocking and check rotations here :3
            Health -= (float)source.CalculateDamage();
        }

        private void CheckAndApplyStatusEffects() {
            if(ActiveEffects.Count < 1) { return; }
            Debug.Log("Effect applied");
        }
    }
}