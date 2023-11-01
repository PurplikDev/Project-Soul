using roguelike.system.singleton;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace roguelike.core.item {
    public class ItemManager : PersistentSingleton<ItemManager> {

        private static Dictionary<string, Item> _itemDatabase = new Dictionary<string, Item>();

        public static List<Sprite> ItemIcons;
        public static Sprite MissingTexture;

        protected override void Awake() {
            base.Awake();
            MissingTexture = Resources.Load<Sprite>("sprites/missing_texture");
            ItemIcons = Resources.LoadAll<Sprite>("sprites/items").ToList();

            RegisterItems();
        }

        private void Register(string id) {
            _itemDatabase.Add(id, new Item(id));
        }

        private void Register(string id, int maxStackSize) {
            _itemDatabase.Add(id, new Item(id, maxStackSize));
        }

        public static Sprite GetSpriteByID(string id) {
            var sprite =  ItemIcons.FirstOrDefault(sprite => sprite.name.Equals(id));
            if(sprite == null) { sprite = MissingTexture; }
            return sprite;
        }

        public static Item GetItemByID(string id) {
            var item = _itemDatabase[id];
            return item;
        }



        private void RegisterItems() {
            Register("air");
            Register("coins", 32);
            Register("test", 32);
            Register("test2");
            Register("test3");
            Register("test4", 6);
        }
    }
}