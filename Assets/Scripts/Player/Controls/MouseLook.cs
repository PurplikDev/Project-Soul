using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivityMouse = 100f;
    public Transform playerBody;
    public Transform playerHead;
    float rotationX = 0f;
    float rotationY = 0f;

    float bodyRotationY;

    private PlayerMovement playerMovement;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        bodyRotationY = playerBody.rotation.y;

        playerBody.rotation = Quaternion.Euler(0f, rotationY, 0f);

        playerMovement = transform.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

        rotationY += mouseX;

        if (Input.GetAxis("Vertical") != 0) {
            playerBody.rotation = Quaternion.Euler(0f, rotationY, 0f);
        } else {
            if (rotationY - bodyRotationY > 30f) {
                playerBody.rotation = Quaternion.Euler(0f, bodyRotationY += 1f, 0f);
            } else if (rotationY - bodyRotationY < -30f) {
                playerBody.rotation = Quaternion.Euler(0f, bodyRotationY -= 1f, 0f);
            }
        }



        playerHead.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
