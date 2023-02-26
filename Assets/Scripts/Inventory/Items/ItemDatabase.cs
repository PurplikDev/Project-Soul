using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] itemObjects;
    public Dictionary<int, ItemObject> getItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < itemObjects.Length; i++)
        {
            itemObjects[i].ID = i;
            getItem.Add(i, itemObjects[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        getItem = new Dictionary<int, ItemObject>();
    }
}
