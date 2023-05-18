using UnityEditor;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] string id;
        public string ID { get { return id; } }
        public string itemName;
        [Range(1, 32)]
        public int maxStackSize = 1;
        [Space]
        public Sprite icon;
        public Transform model;

        private void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            id = AssetDatabase.AssetPathToGUID(path);
        }

        public virtual Item GetCopy()
        {
            return this;
        }

        public virtual void Destory()
        {

        }
    }
}