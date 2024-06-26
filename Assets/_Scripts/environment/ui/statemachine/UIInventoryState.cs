using roguelike.rendering.ui;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.ui.statemachine { 
    public class UIInventoryState: UIBaseState {

        private GameObject _inventoryUIHolder;
        InventoryRenderer _inventoryRenderer;

        public UIInventoryState(UIStateMachine uiStateMachine, GameObject inventoryUIHolder) : base(uiStateMachine) {
            _inventoryUIHolder = inventoryUIHolder;
        }

        public override void EnterState() {
            _inventoryUIHolder.SetActive(true);
            _inventoryRenderer = new InventoryRenderer(GameManager.Player.Inventory, _inventoryUIHolder.GetComponent<UIDocument>());
        }

        public override void ExitState() {
            _inventoryRenderer.Close();
            _inventoryUIHolder.SetActive(false);
            _inventoryRenderer = null;
        }

        public override void UpdateState() { }
    }
}
