using UnityEngine;
using static io.purplik.ProjectSoul.Entity.LivingEntity;

namespace io.purplik.ProjectSoul.Entity.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        private float movementSpeed;
        public float sprintSpeedMultiplier, crouchSpeedMultiplier;
        [Space]
        float horizontalInput, verticalInput;
        [Space]
        public float groundDrag;
        [Space]
        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump = true;
        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask groundMask;
        private bool grounded;
        [Header("Crouching Stuff")]
        public float crouchYScale;
        private float startYScale;
        private CapsuleCollider hitbox;
        [Space]
        public Transform orientation;
        private new Rigidbody rigidbody;
        [Space] 
        public LivingEntity livingEntity;
        public MovementState movementState;
        [Space]
        public bool lockMovement = false;

        Vector3 moveDirection;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.freezeRotation = true;
            livingEntity = GetComponent<LivingEntity>();

            hitbox = GetComponentInChildren<CapsuleCollider>();
            startYScale = hitbox.height;
        }

        void Update()
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

            ProcessInputs();
            SpeedCap();
            StateHandler();

            livingEntity.animator.SetFloat("Speed", rigidbody.velocity.magnitude);

            rigidbody.drag = grounded ? groundDrag : 0;
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        private void ProcessInputs()
        {
            if(lockMovement) { return; }

            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if(Input.GetKeyDown(PlayerKeybinds.jump) && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }

            if(Input.GetKeyDown(PlayerKeybinds.crouch)) {
                hitbox.height = crouchYScale;
            }
            if (Input.GetKeyUp(PlayerKeybinds.crouch)) {
                hitbox.height = startYScale;
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

        private void ResetJump() => readyToJump = true;

        private void StateHandler()
        {
            // SETTING STATE TO SPRINTING
            if(grounded && Input.GetKey(PlayerKeybinds.sprint))
            {
                movementState = MovementState.SPRINTING;
                movementSpeed = livingEntity.speed.Value * sprintSpeedMultiplier;
            } 
            
            // SETTING STATE TO CROUCHING
            else if(grounded && Input.GetKey(PlayerKeybinds.crouch))
            {
                movementState = MovementState.CROUCHING;
                movementSpeed = livingEntity.speed.Value * crouchSpeedMultiplier;
            }
            
            // SETTING STATE TO WALKING
            else if(grounded) 
            {
                movementState = MovementState.WALKING;
                movementSpeed = livingEntity.speed.Value;
            }
            
            // SETTING STATE TO AIR
            else if (!grounded)
            {
                movementState = MovementState.AIR;
            } else
            {
                livingEntity.entityState = EntityState.IDLE;
            }
        }

        public enum MovementState
        {
            SPRINTING,
            WALKING,
            CROUCHING,
            AIR,
            IDLE
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
        public static KeyCode crouch = KeyCode.LeftControl;

        [Header("Attack Keybinds")]
        public static KeyCode primaryAction = KeyCode.Mouse0;
        public static KeyCode secondaryAction = KeyCode.Mouse1;

        [Header("Functional Keybinds")]
        public static KeyCode openInventory = KeyCode.E;
        //public static KeyCode switchBackSlot = KeyCode.F;
        //public static KeyCode switchFocus = KeyCode.R;

        [Header("Misc Keybinds")]
        public static KeyCode pauseGame = KeyCode.Escape;
    }
}