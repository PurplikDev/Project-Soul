using UnityEngine;

namespace roguelike.core.item {
    public class Item {
        private string _id;
        private int _maxStackSize;
        private int _itemValue;

        public Item(string id, int maxStackSize, int itemValue = 0) {
            _id = id;
            _maxStackSize = maxStackSize;
            _itemValue = itemValue;
        }

        public Item(string id, int itemValue = 0) : this(id, 1, itemValue) {
            // hi. :3
        }

        public string ID { get { return _id; } }
        public string Name { get { return TranslationManager.getTranslation(_id); } }
        public Sprite Icon { get { return ItemManager.GetSpriteByID(ID); } }
        public int MaxStackSize { get { return _maxStackSize; } }
        public int ItemValue { get { return _itemValue; } }
    }

    public class ItemStack {
        private Item _stackItem;
        private int _stackSize;

        public ItemStack(Item item, int amount) {
            _stackItem = item;
            _stackSize = amount > _stackItem.MaxStackSize ? _stackItem.MaxStackSize : amount;
        }

        public ItemStack(Item item) : this(item, 1) {
            // hi again :3
        }

        public int IncreaseStackSize(int amount) {
            int combined = _stackSize + amount;
            if(combined > _stackItem.MaxStackSize) {
                int overflow = combined - _stackItem.MaxStackSize;
                _stackSize = combined - overflow;
                return overflow;
            } else {
                _stackSize = combined;
                return 0;
            }
        }

        public void DecreaseStackSize(int amount) {
            int removed = _stackSize - amount;
            if(removed == 0) {
                _stackItem = ItemManager.GetItemByID("air");
                _stackSize = 1;
            } else {
                _stackSize = removed;
            }
        }

        public void SetStackSize(int amount) {
            if(amount <= 0) {
                _stackItem = ItemManager.GetItemByID("air");
            }
            if(amount > _stackItem.MaxStackSize) {
                _stackSize = _stackItem.MaxStackSize;
            } else {
                _stackSize = amount;
            }

        }

        public Item Item { get { return _stackItem; } }
        public int StackSize { get { return _stackSize; } }

        public bool IsEmpty() {
            return _stackItem == ItemManager.GetItemByID("air");
        }

        public static readonly ItemStack EMPTY = new ItemStack(ItemManager.GetItemByID("air"));

        public override string ToString() {
            return _stackItem.Name + " | " + _stackSize;
        }
    }
}