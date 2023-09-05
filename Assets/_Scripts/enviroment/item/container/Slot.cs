using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace roguelike.enviroment.item.container {
    public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {

        public event Action<Slot> OnRightClickEvent;
        public event Action<Slot> OnPointerEnterEvent;
        public event Action<Slot> OnPointerExitEvent;
        public event Action<Slot> OnBeginDragEvent;
        public event Action<Slot> OnEndDragEvent;
        public event Action<Slot> OnDragEvent;
        public event Action<Slot> OnDropEvent;


        private Color visibleColor = new Color(1, 1, 1, 1);
        private Color hiddenColor = new Color(0, 0, 0, 0);

        [SerializeField] Image image;

        private ItemStack _itemStack;
        public ItemStack ItemStack {
            get { return _itemStack; }
            set {
                _itemStack = value;

                if (_itemStack == null) {
                    _itemStack = ItemStack.EMPTY;
                    image.color = hiddenColor;
                } else {
                    image.sprite = _itemStack.Item.Icon;
                    image.color = visibleColor;
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public void OnDrag(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public void OnDrop(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public void OnEndDrag(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public void OnPointerClick(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }
    }
}