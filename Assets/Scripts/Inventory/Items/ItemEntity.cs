using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour
{
    public ItemObject item;
    private Transform model;

    [Range(1, 32)]
    public int itemAmount = 1;

    private float itemRotationY;

    void Awake()
    {
        model = gameObject.GetComponentInChildren<Transform>();
    }

    void Update()
    {
        model.transform.rotation = Quaternion.Euler(0, itemRotationY += Time.deltaTime * 50, 0);
    }
}
