using System;
using UnityEngine;

namespace roguelike.enviroment.item {
    [Serializable]
    public class Item {

        [SerializeField] string _id;
        [SerializeField] int _maxStackSize;
        Sprite _sprite;

        public Item(string id, int maxStackSize)
        {
            _id = id;
            _maxStackSize = maxStackSize;
        }

        public Item(string id) : this(id, 1) {}

        // getters and setters
        public string ItemName { get { return TranslationManager.getTranslation(ID); } }
        public string ID { get { return _id; } }
        public int MaxStackSize { get { return _maxStackSize; } }
        public Sprite Icon { get { return _sprite; } }
    }
}