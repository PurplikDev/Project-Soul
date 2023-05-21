using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    void Update()
    {
        Camera.main.transform.position = transform.position;
    }
}
