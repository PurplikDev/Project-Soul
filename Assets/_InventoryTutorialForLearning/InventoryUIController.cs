using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.item.renderer {
    public class InventoryUIController : MonoBehaviour {
        public List<ItemSlot> InventoryItems = new List<ItemSlot>();

        private VisualElement _root;
        private VisualElement _slotContainer;

        private void Awake() {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _slotContainer =_root.Q<VisualElement>("InventorySlotContainer");

            Debug.Log(_slotContainer.childCount);
        }
    }
}