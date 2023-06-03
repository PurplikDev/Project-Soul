using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelHolderFollow : MonoBehaviour
{
    [SerializeField] Transform follow;
    void Update()
    {
        transform.position = follow.position;
        transform.rotation = follow.rotation;
    }
}
