using UnityEngine;

namespace io.purplik.ProjectSoul.Entity.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        float sensitivityX, sensitivityY;

        [Range(0f, 500f)]
        public float sensitivity;

        public Transform orientation;
        public Transform head;

        float xRotation, yRotation;

        public bool lockRotation = false;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            if(lockRotation)
            {
                return;
            }

            sensitivityX = sensitivity;
            sensitivityY = sensitivity;

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -80, 80);

            head.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}