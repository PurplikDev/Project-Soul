using roguelike.core.item;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.slot {
    public class WeaponSlot : EquipmentSlot {
        public override bool SetStack(ItemStack stack) {

            if (stack.IsEmpty()) {
                return base.SetStack(stack);
            }

            if (stack.Item is WeaponItem weapon) {
                ItemStack otherStack = GetOtherStack();
                if (weapon.IsTwoHanded) {
                    if (otherStack.IsEmpty()) {
                        return base.SetStack(stack);
                    } else {
                        return false;
                    }
                }

                if(otherStack.Item is WeaponItem otherWeapon && otherWeapon.IsTwoHanded) {
                    return false;
                }
            }
            return base.SetStack(stack);
        }
        
        private ItemStack GetOtherStack() {
            return Renderer.itemSlots[
                (int)(SlotEquipmentType == EquipmentType.MAIN_HAND ? EquipmentType.OFF_HAND : EquipmentType.MAIN_HAND)
                ].SlotStack;
        }

        public new class UxmlFactory : UxmlFactory<WeaponSlot, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits {
            protected UxmlEnumAttributeDescription<EquipmentType> slotEquipmentType = new UxmlEnumAttributeDescription<EquipmentType> {
                name = "slot-equipment-type",
                obsoleteNames = new string[1] { "slotEquipmentType" }
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
                base.Init(ve, bag, cc);
                var ate = ve as EquipmentSlot;

                ate.SlotEquipmentType = slotEquipmentType.GetValueFromBag(bag, cc);
            }
        }
    }
}