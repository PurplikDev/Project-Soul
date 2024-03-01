using roguelike.environment.entity.npc;
using roguelike.rendering.ui;
using roguelike.system.manager;


namespace roguelike.environment.ui.statemachine { 
    public class UITraderState: UIBaseState {

        private Trader trader;
        private TradeRenderer renderer;

        public UITraderState(UIStateMachine uiStateMachine, Trader trader) : base(uiStateMachine) {
            this.trader = trader;
        }

        public override void EnterState() {
            trader.InteractionScreenHolder.SetActive(true);
            renderer = trader.GetRenderer(GameManager.Player);
        }

        public override void ExitState() {
            trader.InteractionScreenHolder.SetActive(false);
            renderer = null;

            stateMachine.OnTraderExit();
        }

        public override void UpdateState() { }
    }
}
