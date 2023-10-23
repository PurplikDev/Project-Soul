using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace roguelike.enviroment.item.renderer {
    public class ItemSlot : VisualElement {

        public Image ItemImage;
        public Label ItemLabel;

        public ItemSlot() {
            ItemImage = new Image();
            ItemLabel = new Label();

            AddToClassList("itemSlot");

            RegisterCallback<PointerDownEvent>(OnPointerDown);
        }

        public void OnPointerDown(PointerDownEvent _event) {
            Debug.Log(_event.localPosition);
            InventoryController.DragItem(this);
        }

        #region UXML
        [Preserve]
        public new class UxmlFactory : UxmlFactory<ItemSlot, UxmlTraits> { }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}