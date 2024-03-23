using roguelike.core.item;
using roguelike.environment.world.deployable.workstation;
using roguelike.rendering.ui;
using roguelike.rendering.ui.slot;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering {
    public class AltarOfTheBlazeRenderer : DeployableRenderer {

        private ItemSlot _sacrificeSlot;
        private Button _sacrificeButton, _scorchButton;

        private AltarOfTheBlaze _altar;

        public AltarOfTheBlazeRenderer(Inventory entityInventory, AltarOfTheBlaze altar, UIDocument inventoryUI) : base(entityInventory, altar ,inventoryUI) {
            _altar = altar;
            
            _sacrificeSlot = root.Q<ItemSlot>("AltarSacrificeSlot");

            _sacrificeButton = root.Q<Button>("SacrificeButton");
            _scorchButton = root.Q<Button>("ScorchButton");

            TranslationManager.TranslateHeader(root.Q<Label>("AltarHeader"));
            TranslationManager.TranslateHeader(_sacrificeButton.Q<Label>("SacrificeButtonHeader"));
            TranslationManager.TranslateHeader(_scorchButton.Q<Label>("ScorchButtonHeader"));

            RegisterDeployableSlots();

            _scorchButton.clicked += _altar.SaveGame;
        }

        public override void ClickSlot(Vector2 position, ItemSlot originalSlot, int mouseButton) {
            base.ClickSlot(position, originalSlot, mouseButton);

            if(_sacrificeSlot.SlotStack.Item is Trophy) {
                _sacrificeButton.AddToClassList("button");
                _sacrificeButton.RemoveFromClassList("button_inactive");
            } else {
                _sacrificeButton.AddToClassList("button_inactive");
                _sacrificeButton.RemoveFromClassList("button");
            }
        }

        protected override void RegisterDeployableSlots() {
            _sacrificeSlot.SlotIndex = itemSlots.Count;
            _sacrificeSlot.Renderer = this;
            _sacrificeSlot.SetStack(ItemStack.EMPTY);
            itemSlots.Add(_sacrificeSlot);
        }

        protected override void SyncVisualToInternalSingle(ItemSlot clickedSlot) {
            
        }
    }
}