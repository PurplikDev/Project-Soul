using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{
    private Transform item;

    private float itemRotationX;
    private float itemRotationY;
    private float itemRotationZ;

    void Start()
    {
        item = transform;
    }

    void Update()
    {
        item.rotation = Quaternion.Euler(itemRotationX += Time.deltaTime * 50, itemRotationY += Time.deltaTime * 50, itemRotationZ += Time.deltaTime * 0); ;
    }
}
