using roguelike.environment.entity.npc;


namespace roguelike.environment.ui.statemachine { 
    public class UITraderState: UIBaseState {

        private NPC npc;
        private TradeRenderer renderer;

        public UITraderState(UIStateMachine uiStateMachine, NPC npc) : base(uiStateMachine) {
            this.npc = npc;
        }

        public override void EnterState() {
            
        }

        public override void ExitState() {
            
        }

        public override void UpdateState() { }
    }
}
