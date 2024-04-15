using System;
using System.Collections.Generic;
using roguelike.environment.entity.combat;
using roguelike.environment.entity.statsystem;
using roguelike.environment.entity.statuseffect;
using UnityEngine;
using UnityEngine.Events;

namespace roguelike.environment.entity {
    public abstract class Entity : MonoBehaviour {

        public string EntityName;

        [Header("Entity Stats", order = 0)]
        public Stat MaxHealth = new Stat(30);
        public Stat Speed = new Stat(5);
        public Stat Defence = new Stat(0);
        [Space(order = 1)]
        [Header("Class Stats", order = 1)]
        public Stat Templar = new Stat(0);
        public Stat Rogue = new Stat(0);
        public Stat Thaumaturge = new Stat(0);
        [Space(order = 2)]
        [Header("Entity Properties", order = 3)]
        public bool Immortal = false;
        public bool Invisible = false;


        public Dictionary<StatType, Stat> StatByType = new Dictionary<StatType, Stat>();

        public Vector3 Position { get { return transform.position; } }
        public Quaternion Rotation { get { return Quaternion.LookRotation(LookDirection); } }
        public virtual Vector3 LookDirection { get; internal set; } = Vector3.forward;

        public Action DeathEvent;
        public Action HealthUpdate;
        public Action MaxHealthUpdate;

        // these events are used to sound related stuff.... WHY DID I NOT START USING EVENTS FROM THE START?!?!?!?!??!?!?!
        public Action HurtEvent;
        public Action HealEvent;
        public Action MovementStartEvent;
        public Action MovementStopEvent;
        public Action AttackEvent;

        public UnityEvent UnityDeathEvent; // i want this separate, personal reasons or autism, i dunno

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

        public void SetHealth(float amount) {
            
            if(Health > amount) {
                HurtEvent?.Invoke();
            } else {
                HealEvent?.Invoke();
            }

            if(amount <= 0) {
                DeathEvent?.Invoke();
            }
            
            Health = amount;
            HealthUpdate?.Invoke();
            
        }

        public virtual void Heal(float healAmount) {
            Health = healAmount + Health > MaxHealth.Value ? MaxHealth.Value : healAmount + Health;
            HealEvent?.Invoke();
            HealthUpdate?.Invoke();
        }

        public virtual void Damage(DamageSource source) {
            Health -= (float)source.CalculateDamage();
            if(Health <= 0) {
                DeathEvent?.Invoke();
                UnityDeathEvent.Invoke();
            }
            HealthUpdate?.Invoke();
            HurtEvent?.Invoke();
        }
    }
}