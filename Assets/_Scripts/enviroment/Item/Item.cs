using System;
using UnityEngine;

namespace roguelike.enviroment.item {
    [Serializable]
    [CreateAssetMenu(fileName = "New Item", menuName ="Data/Item")]
    public class Item : ScriptableObject {

        [SerializeField] string _id;
        [SerializeField] [Range(1, 32)] int _maxStackSize;
        [SerializeField] Sprite _sprite;

        // getters and setters
        public string ItemName { get { return TranslationManager.getTranslation(ID); } }
        public string ID { get { return _id; } }
        public int MaxStackSize { get { return _maxStackSize; } }
        public Sprite Icon { get { return _sprite; } }
    }
}