using System;
using UnityEngine;

namespace roguelike.enviroment.item {
    [Serializable]
    public class Item {

        string _id;
        int _maxStackSize;

        public Item(string id, int maxStackSize = 32) {
            _id = id;

            _maxStackSize = maxStackSize;
            if (_maxStackSize > 32) {
                _maxStackSize = 32;
                Debug.LogError("Item [" + TranslationManager.getTranslation("item-" + ID) + "] cannot have maximum stack size over 32!");
            }
            
        }

        // getters and setters
            public string ItemName { get { return TranslationManager.getTranslation("item-"+ID); } }
            public string ID { get { return _id; } }
            public int MaxStackSize { get { return _maxStackSize; } }
            public Sprite Icon { get { return Resources.Load<Sprite>("/items/" + _id); } }
    }
}