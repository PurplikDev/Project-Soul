using UnityEngine;
using io.purplik.ProjectSoul.InventorySystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace io.purplik.ProjectSoul.SaveSystem
{
    [CreateAssetMenu]
    public class ItemDatabase : ScriptableObject
    {
        [SerializeField] Item[] items;

        public Item GetItemReference(string itemID)
        {
            foreach (Item item in items)
            {
                if (item.ID == itemID)
                {
                    return item;
                }
            }
            return null;
        }

        public Item GetItemCopy(string itemID)
        {
            Item item = GetItemReference(itemID);
            return item != null ? item.GetCopy() : null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            LoadItems();
        }

        [System.Obsolete]
        private void OnEnable()
        {
            EditorApplication.projectWindowChanged -= LoadItems;
            EditorApplication.projectWindowChanged += LoadItems;
        }

        [System.Obsolete]
        private void OnDisable()
        {
            EditorApplication.projectWindowChanged -= LoadItems;
        }

        private void LoadItems()
        {
            items = FindAssetsByType<Item>("Assets/ScriptableObjects/InventorySystem/Items");
        }

        public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
        {
            string type = typeof(T).Name;

            string[] guids;
            if (folders == null || folders.Length == 0)
            {
                guids = AssetDatabase.FindAssets("t:" + type);
            }
            else
            {
                guids = AssetDatabase.FindAssets("t:" + type, folders);
            }

            T[] assets = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
            return assets;
        }
#endif
    }
}