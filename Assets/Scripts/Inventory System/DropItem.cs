using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace io.purplik.ProjectSoul.InventorySystem {
    public class DropItem : MonoBehaviour, IDropHandler
    {
        public event Action OnDropEvent;
        public void OnDrop(PointerEventData eventData)
        {
            if(OnDropEvent != null)
            {
                OnDropEvent();
            }
        }
    } 
}
