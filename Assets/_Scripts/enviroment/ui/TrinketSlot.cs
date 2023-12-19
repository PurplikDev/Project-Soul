using roguelike.core.item;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.slot {
    public class TrinketSlot : EquipmentSlot {

        public TrinketSlot() {
            SlotEquipmentType = EquipmentType.TRINKET;
        }

        public override bool SetStack(ItemStack stack) {
            return base.SetStack(stack);
        }

        public new class UxmlFactory : UxmlFactory<TrinketSlot, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }
    }
}