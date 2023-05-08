using UnityEngine;

namespace io.purplik.ProjectSoul.Entity.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float movementSpeed;
        float horizontalInput, verticalInput;

        public float groundDrag;

        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump = true;

        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask groundMask;
        private bool grounded;

        public Transform orientation;
        Rigidbody rigidbody;

        Vector3 moveDirection;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.freezeRotation = true;
        }

        void Update()
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

            ProcessInputs();
            SpeedCap();

            rigidbody.drag = grounded ? groundDrag : 0;
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        private void ProcessInputs()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if(Input.GetKeyDown(PlayerKeybinds.jump) && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void MovePlayer()
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if(grounded)
            {
                rigidbody.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
            } else if(!grounded)
            {
                rigidbody.AddForce(moveDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
            }

        }

        private void SpeedCap()
        {
            Vector3 flatVelocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);

            if(flatVelocity.magnitude > movementSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
                rigidbody.velocity = new Vector3(limitedVelocity.x, rigidbody.velocity.y, limitedVelocity.z);
            }
        }

        private void Jump()
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }


    }

    public static class PlayerKeybinds
    {
        [Header("Movement Keybinds")]
        public static KeyCode forward = KeyCode.W;
        public static KeyCode backwards = KeyCode.S;
        public static KeyCode left = KeyCode.A;
        public static KeyCode right = KeyCode.D;
        public static KeyCode jump = KeyCode.Space;
        public static KeyCode sprint = KeyCode.LeftShift;

        [Header("Attack Keybinds")]
        public static KeyCode primaryAction = KeyCode.Mouse0;
        public static KeyCode secondaryAction = KeyCode.Mouse1;

        [Header("Functional Keybinds")]
        public static KeyCode openInventory = KeyCode.E;
        public static KeyCode switchBackSlot = KeyCode.F;
        public static KeyCode switchFocus = KeyCode.R;

        [Header("Misc Keybinds")]
        public static KeyCode pauseGame = KeyCode.Escape;
    }
}