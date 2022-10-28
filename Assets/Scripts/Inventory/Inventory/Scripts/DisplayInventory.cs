using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public GameObject inventoryPrefab;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    Dictionary<ItemStack, GameObject> itemDisplayed = new Dictionary<ItemStack, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.inventory.items.Count; i++)
        {
            ItemStack itemStack = inventory.inventory.items[i];

            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[itemStack.item.ID].icon; obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.transform.Find("ItemAmount").GetComponent<TextMeshProUGUI>().text = itemStack.itemAmount.ToString("n0");
            obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = itemStack.item.name;
            itemDisplayed.Add(itemStack, obj);
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.inventory.items.Count; i++)
        {
            ItemStack itemStack = inventory.inventory.items[i];

            if (itemDisplayed.ContainsKey(itemStack))
            {
                itemDisplayed[itemStack].GetComponentInChildren<TextMeshProUGUI>().text = inventory.inventory.items[i].itemAmount.ToString("n0");
            } else
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[itemStack.item.ID].icon;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.transform.Find("ItemAmount").GetComponent<TextMeshProUGUI>().text = itemStack.itemAmount.ToString("n0");
                obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = itemStack.item.name;
                itemDisplayed.Add(inventory.inventory.items[i], obj);
            }

        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
