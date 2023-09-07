using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.item.container {
    public class Slot {

        Label _itemAmount;
        VisualElement _itemIcon;

        private ItemStack _itemStack;

        public void SetVisualElement(VisualElement visualElement) {
            _itemAmount = visualElement.Q<Label>("itemAmount");
            _itemIcon = visualElement.Q<VisualElement>("itemIcon");
        }
    }
}