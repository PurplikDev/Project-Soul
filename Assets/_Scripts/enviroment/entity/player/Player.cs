using roguelike.core.eventsystem;
using roguelike.core.item;
using roguelike.enviroment.entity.combat;
using roguelike.enviroment.entity.statsystem;
using roguelike.enviroment.ui.statemachine;
using roguelike.system.input;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {

        public Stat Corruption = new Stat(0); // tldr; corruption

        public UIStateMachine UIStateMachine { get; set; }
        public PlayerInteractor PlayerInteractor { get; set; }
        public PlayerInput PlayerInput { get; private set; }
        public Inventory Inventory { get; private set; }

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

        protected override void Update() { PlayerInteractor.UpdateInteractor(); }
        public override void PrimaryAction() { ItemInMainHand?.ItemAction(this); }
        public override void SecondaryAction() { ItemInOffHand?.ItemAction(this); }

        public override void Damage(DamageSource source) {
            base.Damage(source);
            Events.PlayerHeathUpdateEvent.Invoke(new PlayerHealthUpdateEvent(this));
        }
    }
}
