using roguelike.core.item;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.slot {
    public class OfferSlot : ItemSlot {
        public OfferSlot() : base() { }

        public override bool SetStack(ItemStack stack) {
            return false;
        }

        // These two methods need to be separate in order to prevent the mouse item putting shit in the result slot
        public void ForceStack(ItemStack stack) {
            base.SetStack(stack);
        }

        #region UXML
        [Preserve]
        public new class UxmlFactory : UxmlFactory<OfferSlot, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}