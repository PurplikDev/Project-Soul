using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public Image Icon;
    public string ItemGUID = "";

    public InventorySlot() {
        Icon = new Image();
        Add(Icon);

        Icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");

        RegisterCallback<PointerDownEvent>(OnPointerDown);
    }

    private void OnPointerDown(PointerDownEvent evt) {
        if (evt.button != 0 || ItemGUID.Equals("")) {
            return;
        }
        Icon.image = null;
        InventoryUIController.StartDrag(evt.position, this);
    }

    public void HoldItem(ItemDetails item) {
        Icon.image = item.Icon.texture;
        ItemGUID = item.GUID;
    }

    public void DropItem() {
        ItemGUID = "";
        Icon.image = null;
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
