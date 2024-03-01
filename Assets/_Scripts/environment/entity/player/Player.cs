using Newtonsoft.Json;
using roguelike.core.eventsystem;
using roguelike.core.item;
using roguelike.environment.entity.combat;
using roguelike.environment.entity.statsystem;
using roguelike.environment.ui.statemachine;
using roguelike.system.input;
using UnityEngine;

namespace roguelike.environment.entity.player {
    public class Player : Entity {
        private PlayerInput _playerInput;
        [Space]
        [Header("Player Stats")]
        public Stat Corruption = new Stat(0); // tldr; corruption

        public UIStateMachine UIStateMachine { get; set; }
        public PlayerInteractor PlayerInteractor { get; set; }
        public PlayerInput PlayerInput { get {
                if(_playerInput == null) {
                    _playerInput = new PlayerInput();
                }
                return _playerInput;
            }
        }

        public Inventory Inventory { get; private set; }

        [Space]
        [Header("Player UI Elements")]
        public GameObject InventoryScreen;
        public GameObject PauseScreen;


        public HandheldItem ItemInMainHand {
            get {
                var item = Inventory.Items[24].Item;
                if(item is HandheldItem handheldItem) { return handheldItem; }
                return null; } }

        public HandheldItem ItemInOffHand {
            get {
                var item = Inventory.Items[25].Item;
                if(item is HandheldItem handheldItem) { return handheldItem; }
                return null; } }



        protected override void Awake() {
            PlayerInput.Enable();

            Inventory = new Inventory(this);
            PlayerInteractor = GetComponentInChildren<PlayerInteractor>();
            UIStateMachine = GetComponent<UIStateMachine>();

            PlayerInteractor.Player = this;
            base.Awake();
        }

        protected override void Start() {
            Events.PlayerHeathUpdateEvent.Invoke(new PlayerHealthUpdateEvent(this));
            base.Start();
        }

        public override void PrimaryAction() { ItemInMainHand?.ItemAction(this); }
        public override void SecondaryAction() { ItemInOffHand?.ItemAction(this); }

        public override void Damage(DamageSource source) {
            base.Damage(source);
            Events.PlayerHeathUpdateEvent.Invoke(new PlayerHealthUpdateEvent(this));
        }

        public void SetHealth(float amount) {
            Health = amount;
        }
    }

    [System.Serializable]
    public class PlayerData {
        public float Health;
        public Stat MaxHealth, Speed, Defence, Templar, Rogue, Thaumaturge, Corruption;
        public InventoryData PlayerInventory;

        public PlayerData(Player player) {
            Health = player.Health;
            MaxHealth = player.MaxHealth;
            Speed = player.Speed;
            Defence = player.Defence;
            Templar = player.Templar;
            Rogue = player.Rogue;
            Thaumaturge = player.Thaumaturge;
            Corruption = player.Corruption;
            PlayerInventory = new InventoryData(player.Inventory);
        }

        [JsonConstructor]
        public PlayerData(float health, Stat maxHealth, Stat speed, Stat defence, Stat templar, Stat rogue, Stat thaumaturge, Stat corruptuon, InventoryData inventory) {
            Health = health;
            MaxHealth = maxHealth;
            Speed = speed;
            Defence = defence;
            Templar = templar;
            Rogue = rogue;
            Thaumaturge = thaumaturge;
            Corruption = corruptuon;
            PlayerInventory = inventory;
        }
    }
}
