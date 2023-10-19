using System;
using UnityEngine;

namespace roguelike.enviroment.item {
    [Serializable]
    public class ItemStack {

        [SerializeField] private Item _item;
        [SerializeField] private int _itemAmount;

        public ItemStack(Item item, int itemAmount) {
            _item = item;
            _itemAmount = itemAmount;
        }

        public ItemStack(Item item) : this(item, 1) { }

        // getters
        public Item Item { get { return _item; } }
        public int ItemAmount { get { return _itemAmount; } }

        public string ItemAmountDisplay {
            get {
                if(_itemAmount > 1) {
                    return _itemAmount.ToString();
                } else {
                    return "";
                }
            }
        }



        public void increaseStackBy(int amount) {
            _itemAmount += amount;
        }

        public void shrinkStackBy(int amount) {
            _itemAmount -= amount;
        }

        public void setStackTo(int amount) {
            _itemAmount = amount;
        }

        public bool IsEmpty() {
            return Item.ID == "air";
        }
    }
}
