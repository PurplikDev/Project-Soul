using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/SpeedEquipment")]

public class SpeedEquipment : EquipmentEffect
{
    public float amount;

    public override void Equip(GameObject targetEntity) {
        targetEntity.GetComponent<EntityStats>().speed += amount;
    }

    public override void Unequip(GameObject targetEntity) {
        targetEntity.GetComponent<EntityStats>().speed -= amount;
    }
}
