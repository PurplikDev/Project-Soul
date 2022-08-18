using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    private float defaultSpeed;
    private float speed;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform playerBody;
    public Transform playerHead;
    private float headRotationY = 0f;
    private float bodyRotationY = 0f;

    Vector3 velocity;
    bool isGrounded;

    public Animator animator;

    void Update()
    {

        CalculateStats();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);

        animator.SetFloat("speed", move.sqrMagnitude);

        if(Input.GetButton("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void CalculateStats() {

        defaultSpeed = gameObject.GetComponent<EntityStats>().speed;

        if (Input.GetButton("Sprint")) {
            speed = defaultSpeed * 1.45f;
            animator.SetFloat("sprintModifier", 1.45f);
        }
        else
        {
            speed = defaultSpeed;
            animator.SetFloat("sprintModifier", 1f);
        }
    }
}
