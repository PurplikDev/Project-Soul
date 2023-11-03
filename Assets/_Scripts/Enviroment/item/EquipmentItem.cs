namespace roguelike.core.item
{
    public class EquipmentItem : Item
    {
        private EquipmentType _itemEquipmentType;

        public EquipmentType ItemEquipmentType { get { return _itemEquipmentType; } }

        public EquipmentItem(string id, EquipmentType type) : base(id) {
            _itemEquipmentType = type;
        }
    }

    public enum EquipmentType
    {
        HELMET = 20,
        CHESTPLATE = 21,
        PANTS = 22,
        BOOTS = 23,
        MAIN_HAND = 24,
        OFF_HAND = 25,
        TRINKET = 26
    }
}