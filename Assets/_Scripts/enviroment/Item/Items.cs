using UnityEngine;
using System.Collections.Generic;

namespace roguelike.enviroment.item {
    public class Items : MonoBehaviour {

        public static Dictionary<string, Item> items = new Dictionary<string, Item>();

        void Awake() {
            var loadedItems = Resources.LoadAll<Item>("data/items");
            foreach (var item in loadedItems) {
                items.Add(item.ID, item);
            }
        }
    }
}