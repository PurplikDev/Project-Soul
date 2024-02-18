using roguelike.core.eventsystem;
using roguelike.core.item;
using roguelike.environment.entity.combat;
using roguelike.environment.entity.statsystem;
using roguelike.environment.ui.statemachine;
using roguelike.system.input;
using UnityEngine;

namespace roguelike.environment.entity.player {
    public class Player : Entity {
        [Space]
        [Header("Player Stats")]
        public Stat Corruption = new Stat(0); // tldr; corruption

        public UIStateMachine UIStateMachine { get; set; }
        public PlayerInteractor PlayerInteractor { get; set; }
        public PlayerInput PlayerInput { get; private set; }
        public Inventory Inventory { get; private set; }

        [Space]
        [Header("Player UI Elements")]
        public GameObject InventoryScreen;
        public GameObject PauseScreen;
        public GameObject DialogScreen;


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
            PlayerInput = new PlayerInput();
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
    }
}
