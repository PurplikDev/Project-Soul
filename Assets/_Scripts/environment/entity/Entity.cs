using System;
using System.Collections.Generic;
using roguelike.environment.entity.combat;
using roguelike.environment.entity.statsystem;
using UnityEngine;
using static roguelike.environment.entity.statsystem.Stat;

namespace roguelike.environment.entity {
    public abstract class Entity : MonoBehaviour {
        public Stat MaxHealth = new Stat(30);
        public Stat Speed = new Stat(5);
        public Stat Defence = new Stat(0);

        public Stat Templar = new Stat(0);
        public Stat Rogue = new Stat(0);
        public Stat Thaumaturge = new Stat(0);

        public Dictionary<StatType, Stat> StatByType = new Dictionary<StatType, Stat>();

        public List<StatusEffect> ActiveEffects = new List<StatusEffect>();

        public Vector3 Position { get { return transform.position; } }
        public Quaternion Rotation { get { return Quaternion.LookRotation(LookDirection); } }
        public virtual Vector3 LookDirection { get; internal set; } = Vector3.forward;

        public Action DeathEvent;

        public bool Immortal = false;
        public bool Invisible = false;
        public bool IsBlocking { get; internal set; } = false;
        public bool IsDead { get; internal set; } = false;
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

        public virtual void PrimaryAction() {
            Debug.Log("Entity primary action!");
        }

        public virtual void SecondaryAction() {
            Debug.Log("Entity secondary action!");
        }

        public int GetDefenceTier() {
            return Mathf.FloorToInt(Defence.Value);
        }

        public virtual void Damage(DamageSource source) {
            Health -= (float)source.CalculateDamage();
            if(Health <= 0) {
                DeathEvent.Invoke();
            }
        }

        private void CheckAndApplyStatusEffects() {
            if(ActiveEffects.Count < 1) { return; }
            Debug.Log("Effect applied");
        }
    }
}