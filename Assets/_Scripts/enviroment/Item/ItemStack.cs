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

        public static ItemStack EMPTY = new ItemStack(Items.AIR);

        public void shrinkStackBy(int amount) {
            _itemAmount -= amount;
        }

        public void shrinkStackTo(int amount) {
            _itemAmount = amount;
        }
    }
}
