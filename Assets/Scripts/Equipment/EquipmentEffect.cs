using UnityEngine;

public abstract class EquipmentEffect : ScriptableObject
{
    public abstract void Equip(GameObject targetEntity);
    public abstract void Unequip(GameObject targetEntity);
}
