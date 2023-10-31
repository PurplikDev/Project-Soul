using roguelike.core.utils;
using UnityEngine;

namespace roguelike.core.item {
    public class Item {
        private string _id;
        private int _maxStackSize;

        public Item(string id, int maxStackSize) {
            _id = id;
            _maxStackSize = maxStackSize;
        }

        public Item(string id) : this(id, 1) {
            // hi. :3
        }

        public string ID { get { return _id; } }
        public string Name { get { return TranslationManager.getTranslation(_id); } }
        public Sprite Icon { get { return ItemManager.GetSpriteByID(ID); } }
        public int MaxStackSize { get { return _maxStackSize; } }
    }

    public class ItemStack {
        private Item _stackItem;
        private int _stackSize;

        public ItemStack(Item item, int amount) {
            _stackItem = item;
            _stackSize = amount;
        }

        public ItemStack(Item item) : this(item, 1) {
            // hi again :3
        }

        public int IncreaseStack(int amount) {
            int overflow = Mathematicus.OverflowFromAddition(_stackSize, amount, _stackItem.MaxStackSize);
            _stackSize += amount; //(amount - overflow);
            return amount - overflow;
        }

        public Item Item { get { return _stackItem; } }
        public int StackSize { get { return _stackSize; } }
    }
}