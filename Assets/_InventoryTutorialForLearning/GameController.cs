using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemDetails {
    public string Name;
    public string GUID;
    public Sprite Icon;
    public bool CanDrop;
}

public enum InventoryChangeType {
    PICKUP,
    DROP
}

public delegate void OnInventoryChangedDelegate(string[] itemGUID, InventoryChangeType changeType);

public class GameController : MonoBehaviour
{
    [SerializeField] public List<Sprite> IconSprites;
    private static Dictionary<string, ItemDetails> _itemDatabase = new Dictionary<string, ItemDetails>();
    [SerializeField] private List<ItemDetails> _playerInventory = new List<ItemDetails>();
    public static event OnInventoryChangedDelegate OnInventoryChanged = delegate { };

    private void Awake() {
        PopulateDatabase();
    }

    private void Start() {
        _playerInventory.AddRange(_itemDatabase.Values);
        OnInventoryChanged.Invoke(_playerInventory.Select(x => x.GUID).ToArray(), InventoryChangeType.PICKUP);
    }

    public void PopulateDatabase() {
        _itemDatabase.Add("39fc5b3e-71f1-11ee-b962-0242ac120002", new ItemDetails() {
            Name = "Thing",
            GUID = "39fc5b3e-71f1-11ee-b962-0242ac120002",
            Icon = IconSprites.FirstOrDefault(x => x.name.Equals("missing_texture")),
            CanDrop = false
        });

        _itemDatabase.Add("277033fe-71f2-11ee-b962-0242ac120002", new ItemDetails() {
            Name = "Thing2",
            GUID = "277033fe-71f2-11ee-b962-0242ac120002",
            Icon = IconSprites.FirstOrDefault(x => x.name.Equals("missing_texture")),
            CanDrop = true
        });

        _itemDatabase.Add("2f63fc12-71f2-11ee-b962-0242ac120002", new ItemDetails() {
            Name = "Thing3",
            GUID = "2f63fc12-71f2-11ee-b962-0242ac120002",
            Icon = IconSprites.FirstOrDefault(x => x.name.Equals("missing_texture")),
            CanDrop = true
        });

        _itemDatabase.Add("373f883e-71f2-11ee-b962-0242ac120002", new ItemDetails() {
            Name = "Thing4",
            GUID = "373f883e-71f2-11ee-b962-0242ac120002",
            Icon = IconSprites.FirstOrDefault(x => x.name.Equals("missing_texture")),
            CanDrop = true
        });
    }    

    public static ItemDetails GetItemByGUID(string guid) {
        if(_itemDatabase.ContainsKey(guid)) {
            return _itemDatabase[guid];
        }

        return null;
    }
}
