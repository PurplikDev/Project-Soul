using UnityEngine;
using UnityEngine.UIElements;

public class InventoryRenderer : MonoBehaviour
{
    private VisualTreeAsset _itemSlot;

    private void Awake() {
            _itemSlot = Resources.Load<VisualTreeAsset>("ui/itemslot.uxml");
    }


}
