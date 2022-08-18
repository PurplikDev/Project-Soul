using UnityEngine;

public class Equipment : MonoBehaviour
{

    public EquipmentEffect equipmentEffect;

    private void OnTriggerEnter(Collider collision) {
        Destroy(gameObject);
        equipmentEffect.Equip(collision.gameObject);
    }
}
