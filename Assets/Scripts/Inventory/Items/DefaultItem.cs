using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Item", menuName = "Inventory/Items/Default Item")]
public class DefaultItem : ItemObject
{
    void Awake()
    {
        type = ItemType.GENERIC;
    }
}
