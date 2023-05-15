using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        public event Action<ItemSlot> OnRightClickEvent;

        public event Action<ItemSlot> OnPointerEnterEvent;
        public event Action<ItemSlot> OnPointerExitEvent;

        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;

        public event Action<ItemSlot> OnDragEvent;

        public event Action<ItemSlot> OnDropEvent;


        private Color visibleColor = new Color(1, 1, 1, 1);
        private Color hiddenColor = new Color(0, 0, 0, 0);
        private Item _item;
        public Item item
        {
            get { return _item; }
            set
            {
                _item = value;

                if (_item == null && itemAmount != 0) itemAmount = 0;

                if(_item == null)
                {
                    image.color = hiddenColor;
                } else
                {
                    image.sprite = _item.icon;
                    image.color = visibleColor;
                }
            }
        }

        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI amountText;

        private int _itemAmount;

        public int itemAmount
        {
            get { return _itemAmount; }
            set
            {
                _itemAmount = value;
                if (_itemAmount < 0) _itemAmount = 0;
                if (_itemAmount == 0 && _item != null) item = null;


                if (amountText != null)
                {
                    amountText.enabled = _item != null && _itemAmount > 1;
                    if (amountText.enabled)
                        amountText.text = _itemAmount.ToString();
                }
            }
        }

        protected virtual void OnValidate()
        {
            if(image == null)
                image = GetComponentInChildren<Image>();

            if(amountText == null)
                amountText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public virtual bool CanAddStack(Item __item, int amount = 1)
        {
            return item != null && item.ID == __item.ID && itemAmount + amount <= item.maxStackSize;
        }

        public virtual bool IsValidItem(Item item)
        {
            return true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData != null && eventData.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift))
            {
                OnRightClickEvent(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (OnPointerEnterEvent != null)
            {
                OnPointerEnterEvent(this);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (OnPointerExitEvent != null)
            {
                OnPointerExitEvent(this);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (OnDragEvent != null)
            {
                OnDragEvent(this);
            }
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (OnBeginDragEvent != null)
            {
                OnBeginDragEvent(this);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (OnEndDragEvent != null)
            {
                OnEndDragEvent(this);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (OnDropEvent != null)
            {
                OnDropEvent(this);
            }
        }
    }
}

