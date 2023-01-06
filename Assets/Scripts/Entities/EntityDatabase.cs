using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Database", menuName = "Entity/Entity Database")]
public class EntityDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public EntityObject[] entities;
    public Dictionary<int, EntityObject> getEntity = new Dictionary<int, EntityObject>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < entities.Length; i++)
        {
            entities[i].entityID = i;
            getEntity.Add(i, entities[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        getEntity = new Dictionary<int, EntityObject>();
    }
}
