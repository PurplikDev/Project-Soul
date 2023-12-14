using roguelike.core.item;
using UnityEngine.UIElements;

namespace roguelike.rendering.ui.slot {
    public class WeaponSlot : EquipmentSlot {
        public override bool SetStack(ItemStack stack) {

            if(stack.IsEmpty()) {
                return base.SetStack(stack);
            }

            if(stack.Item is not WeaponItem weaponItem) {
                return false;
            }

            ItemStack otherStack = GetStackFromSlot((int)GetOtherSlot());

            if(weaponItem.IsTwoHanded) {
                if(!otherStack.IsEmpty()) {
                    return false;
                }
            } if(((WeaponItem)otherStack.Item).IsTwoHanded) {
                return false;
            }

            return base.SetStack(stack);
        }

        private ItemStack GetStackFromSlot(int slot) {
            return Renderer.itemSlots[slot].SlotStack;
        }

        private Inventory.InventorySlot GetOtherSlot() {
            return SlotEquipmentType == EquipmentType.MAIN_HAND ? Inventory.InventorySlot.MAIN_HAND : Inventory.InventorySlot.OFF_HAND;
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