using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
    }
}