namespace io.purplik.ProjectSoul.InventorySystem {
    public class EquipmentSlot : ItemSlot
    {
        public EquipmentType equipmentType;

        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.name = equipmentType.ToString() + " Slot";
        }

        public override bool IsValidItem(Item item)
        {
            if(item == null)
            {
                return true;
            }

            EquipmentItem equipmentItem = item as EquipmentItem;
            return equipmentItem != null && equipmentItem.equipmentType == equipmentType;
        }
    } 
}
