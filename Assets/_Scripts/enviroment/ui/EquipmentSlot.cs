using roguelike.core.item;
using UnityEngine.UIElements;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType SlotEquipmentType { get; set; }

    public EquipmentSlot() : base() {} // todo: add background images for equipment slot to show what equipment type it is

    public override bool SetStack(ItemStack stack) {
        if (stack.IsEmpty() || stack.Item is EquipmentItem equipmentItem && equipmentItem.ItemEquipmentType == SlotEquipmentType)
        {
            // a small thing, but it makes the equipment slot visual feel better (making the background trinket type indicator invisible)

            // todo: trinket slot types

            StyleColor imageTint = new StyleColor();
            imageTint.value = new UnityEngine.Color(255, 255, 255, stack.IsEmpty() ? 0.25f : 0); // swag code :3
            style.unityBackgroundImageTintColor = imageTint;
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
            obsoleteNames = new string[1] { "slotEquipmentType" }
        };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as EquipmentSlot;

            ate.SlotEquipmentType = slotEquipmentType.GetValueFromBag(bag, cc);
        }
    }
}
