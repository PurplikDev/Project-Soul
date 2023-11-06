using roguelike.core.item;
using UnityEngine;
using UnityEngine.UIElements;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType SlotEquipmentType { get; set; }

    public EquipmentSlot() : base() {
        //style.backgroundImage = Resources.Load<Sprite>("sprites/missing_texture").texture;
    }

    public override bool SetStack(ItemStack stack) {
        if (stack.IsEmpty() || stack.Item is EquipmentItem equipmentItem && equipmentItem.ItemEquipmentType == SlotEquipmentType)
        {
            return base.SetStack(stack);
        }
        return false;
    }

    public new class UxmlFactory : UxmlFactory<EquipmentSlot, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        protected UxmlEnumAttributeDescription<EquipmentType> slotEquipmentType = new UxmlEnumAttributeDescription<EquipmentType>
        {
            name = "slot-equipment-type",
            obsoleteNames = new string[1] {
                "slotEquipmentType"
            }
        };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as EquipmentSlot;

            ate.SlotEquipmentType = slotEquipmentType.GetValueFromBag(bag, cc);
        }
    }
}