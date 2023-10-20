using roguelike.enviroment.item.equipment;

namespace roguelike.enviroment.item.container {
    public class EquipmentSlot : Slot {
        public EquipmentType EquipmentType;

        public EquipmentSlot(EquipmentType equipmentType) {
            EquipmentType = equipmentType;
        }
    }
}

