using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Item _item;

        public event Action<Item> OnRightClickEvent;

        public Item item
        {
            get { return _item; }
            set
            {
                _item = value;
                if(_item == null)
                {
                    image.enabled = false;
                } else
                {
                    image.sprite = _item.icon;
                    image.enabled = true;
                }
            }
        }

        [SerializeField] private Image image;

        protected virtual void OnValidate()
        {
            if(image == null)
            {
                image = GetComponent<Image>();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                OnRightClickEvent(item);
            }
        }
    }
}

