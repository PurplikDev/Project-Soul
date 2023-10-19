using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace roguelike.enviroment.item.renderer {
    public class ItemSlot : VisualElement {

        public ItemStack SlotItemStack;

        public Image ItemImage;
        public Label ItemLabel;

        public ItemSlot() {
            ItemImage = new Image();
            ItemLabel = new Label();

            AddToClassList("itemSlot");

            RegisterCallback<PointerDownEvent>(OnPointerDown);
        }

        public void OnPointerDown(PointerDownEvent _event) {

        }

        #region UXML
        [Preserve]
        public new class UxmlFactory : UxmlFactory<ItemSlot, UxmlTraits> { }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}